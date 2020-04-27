using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDeviceComponent : MonoBehaviour, IOutline, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BaseDevice baseDevice  => GetComponentInParent<BaseDevice>();
    public virtual void HandleInteraction()
    {
        try
        {
            baseDevice.HandleBaseDeviceComponentMessage(this);
        }
        catch 
        {
            Debug.LogError(name + " does not have a baseDevice in parent gameObject");
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.clickCount)
        {
            case 1:
                break;
            case 2:
                OnDoubleClick();
                break;
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
    protected virtual void OnClick()
    {
        Debug.Log(name + ": OnClick() called.");
    }
    protected virtual void OnDoubleClick()
    {
        Debug.Log(name + ": OnDoubleClick() called.");
    }

    public void AddOutline()
    {
        Outline.AddTo(this);
    }

    public void RemoveOutline()
    {
        Outline.RemoveFrom(this);
    }
}
