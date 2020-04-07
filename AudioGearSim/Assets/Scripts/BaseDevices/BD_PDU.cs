using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_PDU : BaseDevice
{
    public State state { get; private set; }

    public override string[] _ports => new string[] { 
        TransmissionType.Power.ToString()+"In",
        TransmissionType.Power.ToString()+"Out1",
        TransmissionType.Power.ToString()+"Out2",
        TransmissionType.Power.ToString()+"Out3",
        TransmissionType.Power.ToString()+"Out4",
        TransmissionType.Power.ToString()+"Out5",
        TransmissionType.Power.ToString()+"Out6",
    };
    
    void TurnOff()
    { 
    
    }

    void TurnOn()
    { 
    
    }
    public override void HandleBaseDeviceComponentMessage(BaseDeviceComponent baseDeviceComponent)
    {
        throw new System.NotImplementedException();
    }
    public override void HandlePortConnectionMessage()
    {
        throw new System.NotImplementedException();
    }

    protected override void TransmissionReceived(object SenderBaseDevice, TransmissionArgs TransmissionArgs)
    {
        throw new System.NotImplementedException();
    }

    public enum State { Off, On}
}
