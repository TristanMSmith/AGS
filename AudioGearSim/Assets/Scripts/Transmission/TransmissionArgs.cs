using AGS;

public class TransmissionArgs
{
    public TransmissionProtocol transmissionProtocol { get; set; }
    public TransmissionArgs(TransmissionProtocol TransmissionProtocol)
    {
        transmissionProtocol = TransmissionProtocol;
    }
}
