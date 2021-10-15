using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    class SendCommunicationCommand
    {
        public string Template { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
