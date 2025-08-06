using AppForeach.Framework.Logging;

namespace AppForeach.Framework.Hosting.Features.Serilog.Ecs
{
    public class EcsToMetadataPropertyMap : ILoggingPropertyMap
    {
        private static readonly HashSet<string> ecsTopPathes =
            ["agent", "as", "client", "cloud", "code_signature", "container", "data_stream",
            "destination", "device", "dll", "dns", "elf", "email", "error",
            "event", "faas", "file", "geo", "group", "hash", "host",
            "http", "log", "macho", "network", "interface", "orchestrator", "organization",
            "os", "package", "pe", "process", "registry", "related", "risk", "rule", "server",
            "service", "source", "threat", "tls", "url", "user", "user_agent", "vlan", "volume",
            "vulnerability", "x509"];

        public IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            foreach (var kvp in properties)
            {
                int topPropertyEndIndex = kvp.Key.IndexOf('.');

                if (topPropertyEndIndex > 0 
                    && ecsTopPathes.Contains(kvp.Key.Substring(0, topPropertyEndIndex)))
                {
                    yield return kvp;
                }
                else
                {
                    yield return new KeyValuePair<string, object>("metadata." + kvp.Key, kvp.Value);
                }
            }
        }
    }
}
