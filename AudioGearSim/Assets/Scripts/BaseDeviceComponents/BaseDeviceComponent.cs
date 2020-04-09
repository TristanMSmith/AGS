using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDeviceComponent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BaseDevice baseDevice  => GetComponentInParent<BaseDevice>();
    public virtual void HandleInteraction()
    {
        baseDevice.HandleBaseDeviceComponentMessage(this);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnDoubleClick();
        }
}

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnter();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExit();

    }

    protected virtual void OnPointerEnter()
    {
        //add outline shader
        Debug.Log(name + ": OnPointerEnter");
    }
    protected virtual void OnPointerExit()
    {
        //remove outline shader
        Debug.Log(name + ": OnPointerExit");
    }
    protected virtual void OnDoubleClick()
    {
        Debug.Log(name + ": OnDoubleClick() called.");
    }
}
