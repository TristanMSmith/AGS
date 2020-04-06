using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_ValveSpecial : BD_GuitarAmp
{
    State state;
    public BDC_Switch2Way Switch_Power, Switch_Standby;
    public BDC_Dial Gain, Treble, Middle, Bass, DSP, ReverbLevel, MasterVolume;
    public BDC_LED LED_Power;
    public BDC_Speaker Speaker;
    float gain, treble, middle, bass, reverbLevel, masterVolume;
    void UpdateLEDS()
    {
        switch (state)
        {
            case State.Off:
                LED_Power.TurnOff();
                break;
            case State.Standby:
                LED_Power.TurnStandby();
                break;
            case State.On:
                LED_Power.TurnOn();
                break;
        }
    }
    void HandleSwitch2WayInteraction(BDC_Switch2Way switch2Way)
    {
        switch (Switch_Power.state)
        {
            case BDC_Switch2Way.State.Down:
                state = State.Off;
                break;
            case BDC_Switch2Way.State.Up:
                switch (Switch_Standby.state)
                {
                    case BDC_Switch2Way.State.Down:
                        state = State.Standby;
                        break;
                    case BDC_Switch2Way.State.Up:
                        state = State.On;
                        break;
                }
                break;
        }
        UpdateLEDS();
    }
    void HandleKnobInteraction(BDC_Dial dial)
    {
        if (MasterVolume == dial)
        {
            Speaker.GetComponent<AudioSource>().volume = (dial.transform.rotation.x - dial.minRotation) / (dial.maxRotation - dial.minRotation);
        }
    }
    public override void HandleBaseDeviceComponentMessage(BaseDeviceComponent baseDeviceComponent)
    {
        if (baseDeviceComponent is BDC_Switch2Way)
        {
            HandleSwitch2WayInteraction((BDC_Switch2Way)baseDeviceComponent);
        }

        if (baseDeviceComponent is BDC_Dial)
        {
            HandleKnobInteraction((BDC_Dial)baseDeviceComponent);
        }
    }

    public override void HandlePortConnectionMessage()
    {
        throw new System.NotImplementedException();
    }
    public enum State { Off, Standby, On }
    enum DSPOption { Delay, Chorus, Flanger }
}
