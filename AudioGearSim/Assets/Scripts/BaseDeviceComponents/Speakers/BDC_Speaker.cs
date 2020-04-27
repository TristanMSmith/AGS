using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public abstract class BDC_Speaker : BaseDeviceComponent
{
    // Start is called before the first frame update
    void Start()
    {
        AGS.Debug.AudioListenerCheck();
    }
}
