using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public abstract class BDC_Switch : BaseDeviceComponent
{
    protected Animator animator => GetComponentInChildren<Animator>();
    private void Start()
    {
        AGS.Debug.RaycasterCheck();
    }
}
