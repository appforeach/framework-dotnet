using System.Collections.Generic;

namespace FrameworkSagas.Operations
{
    public class SendCommunicationInput
    {
        public string Template { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
