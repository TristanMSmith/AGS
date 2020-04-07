using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDeviceComponent : MonoBehaviour
{
    public BaseDevice baseDevice  => GetComponentInParent<BaseDevice>();
    public virtual void HandleInteraction()
    {
        baseDevice.HandleBaseDeviceComponentMessage(this);
    }
}
