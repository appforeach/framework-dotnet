using AppForeach.Framework.Logging;

namespace AppForeach.Framework.Hosting.Features.Logging;

public class EcsFrameworkLoggingPropertyMap : ILoggingPropertyMap
{
    public IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties)
    {
        int? eventId = null;
        string? eventName = null;

        foreach (var property in properties)
        {
            switch (property.Key)
            {
                case FrameworkLogProperties.EventId when property.Value is int eventIdValue:
                    eventId = eventIdValue;
                    break;
                case FrameworkLogProperties.EventName when property.Value is string eventNameValue:
                    eventName = eventNameValue;
                    break;
                case FrameworkLogProperties.Logger:
                    yield return new KeyValuePair<string, object>("log.logger", property.Value);
                    break;
                case FrameworkLogProperties.OperationName:
                    yield return new KeyValuePair<string, object>("event.action", property.Value);
                    break;
                case FrameworkLogProperties.OperationKind:
                    yield return new KeyValuePair<string, object>("event.kind", "event");
                    yield return new KeyValuePair<string, object>("event.category", "api");
                    const string mappedKindProperty = "event.type";

                    switch (property.Value)
                    {
                        case "Command":
                            yield return new KeyValuePair<string, object>(mappedKindProperty, "change");
                            break;
                        case "Query":
                            yield return new KeyValuePair<string, object>(mappedKindProperty, "info");
                            break;
                    }
                    break;
                case FrameworkLogProperties.OperationDuration when property.Value is double elapsedValue:
                    long nanoElapsed = (long)(elapsedValue * 1000000);
                    yield return new KeyValuePair<string, object>("event.duration", nanoElapsed);
                    break;
                case FrameworkLogProperties.OperationOutcome:
                    const string mappedProperty = "event.outcome";

                    switch (property.Value)
                    {
                        case "Success":
                            yield return new KeyValuePair<string, object>(mappedProperty, "success");
                            break;
                        case "Error":
                            yield return new KeyValuePair<string, object>(mappedProperty, "failure");
                            break;
                        default:
                            yield return new KeyValuePair<string, object>(mappedProperty, "unknown");
                            break;
                    }

                    break;
                case FrameworkLogProperties.ErrorMessage:
                    yield return new KeyValuePair<string, object>("error.message", property.Value);
                    break;
                case FrameworkLogProperties.ErrorType:
                    yield return new KeyValuePair<string, object>("error.type", property.Value);
                    break;
                case FrameworkLogProperties.ErrorStackTrace:
                    yield return new KeyValuePair<string, object>("error.stack_trace", property.Value);
                    break;
                case FrameworkLogProperties.ErrorId:
                    yield return new KeyValuePair<string, object>("error.id", property.Value);
                    break;
                default:
                    yield return property;
                    break;
            }
        }

        if(eventName is not null)
        {
            yield return new KeyValuePair<string, object>("event.code", eventName);
        }
        else if(eventId is not null)
        {
            yield return new KeyValuePair<string, object>("event.code", eventId.Value.ToString());
        }
    }
}
