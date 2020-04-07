using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Tuner : BD_GuitarPedal
{
    public override string[] _ports => new string[] { 
        TransmissionType.Power.ToString()+"In",
        TransmissionType.MonoBalanced.ToString()+"In", 
        TransmissionType.MonoBalanced.ToString() + "Out", 
        TransmissionType.MonoBalanced.ToString() + "Bypass" 
    };

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
}
