using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_PDU : BaseDevice
{
    public State state { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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


    public enum State { Off, On}
}
