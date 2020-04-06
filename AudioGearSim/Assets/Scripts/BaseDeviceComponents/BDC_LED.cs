using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDC_LED : BaseDeviceComponent
{
    public Material offMaterial, onMaterial, standbyMaterial;
    public State state { get; private set; }
    MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
    public void TurnOff()
    {
        meshRenderer.material = offMaterial;
        state = State.Off;
    }
    public void TurnOn()
    {
        meshRenderer.material = onMaterial;
        state = State.On;
    }
    public void TurnStandby()
    {
        meshRenderer.material = standbyMaterial;
        state = State.Standby;
    }
    public enum State { Off, Standby, On}
}
