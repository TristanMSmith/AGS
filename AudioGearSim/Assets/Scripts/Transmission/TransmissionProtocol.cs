using System;

namespace AGS
{
    public class TransmissionProtocol
    {
        public DateTime TimeStamp { get; set; }
        public string SenderBaseDevice { get; set; }
        public string SenderPort { get; set; }
        public string RecipientBaseDevice { get; set; }
        public string RecipientPort { get; set; }
        public TransmissionType TransmissionType { get; protected set; }
    }
}