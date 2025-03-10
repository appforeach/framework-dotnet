using AppForeach.Framework.Logging;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Logging;

public class EcsLoggingPropertyMap : ILoggingPropertyMap
{
    public Dictionary<string, object> MapProperties(Dictionary<string, object> properties)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        foreach (var property in properties)
        {
            MapProperty(property, result);
        }

        return result;
    }

    private void MapProperty(KeyValuePair<string, object> property, Dictionary<string, object> result)
    {
        switch (property.Key)
        {
            case FrameworkLogProperties.Logger:
                result["log.logger"] = property.Value;
                break;
            case FrameworkLogProperties.OperationName:
                result["event.action"] = property.Value;
                break;
            case FrameworkLogProperties.OperationKind:
                result["event.kind"] = "event";
                result["event.category"] = "api";
                const string mappedKindProperty = "event.type";

                switch(property.Value)
                {
                    case "Command":
                        result[mappedKindProperty] = "change";
                        break;
                    case "Query":
                        result[mappedKindProperty] = "info";
                        break;
                }
                break;
            case FrameworkLogProperties.OperationDuration:
                result["event.duration"] = property.Value;
                break;
            case FrameworkLogProperties.OperationOutcome:
                const string mappedProperty = "event.outcome";
                
                switch(property.Value)
                {
                    case "Success":
                        result[mappedProperty] = "success";
                        break;
                    case "Error":
                        result[mappedProperty] = "failure";
                        break;
                    default:
                        result[mappedProperty] = "unknown";
                        break;
                }
                
                break;
            case FrameworkLogProperties.ErrorMessage:
                result["error.message"] = property.Value;
                break;
            case FrameworkLogProperties.ErrorType:
                result["error.type"] = property.Value;
                break;
            case FrameworkLogProperties.ErrorStackTrace:
                result["error.stack_trace"] = property.Value;
                break;
            case FrameworkLogProperties.ErrorId:
                result["error.id"] = property.Value;
                break;
        }
    }
}
