
namespace AppForeach.Framework.Logging
{
    public class FrameworkLogEvents
    {
        public static readonly FrameworkLogEventId OperationStarted = new FrameworkLogEventId(10101, nameof(OperationStarted));

        public static readonly FrameworkLogEventId OperationCompleted = new FrameworkLogEventId(10102, nameof(OperationCompleted));

        public static readonly FrameworkLogEventId UnhandledException = new FrameworkLogEventId(10201, nameof(UnhandledException));

        public static readonly FrameworkLogEventId ValidationFailed = new FrameworkLogEventId(10301, nameof(ValidationFailed));
    }
}
