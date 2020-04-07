using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public abstract class BDC_Speaker : BaseDeviceComponent
{
    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main.GetComponent<AudioListener>() == null)
        {
            Debug.Log("Camera doesn't have an audio listener.");
        }
    }
}
