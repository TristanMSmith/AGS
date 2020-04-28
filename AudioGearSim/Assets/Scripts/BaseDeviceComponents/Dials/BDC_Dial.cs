using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public abstract class BDC_Dial : BaseDeviceComponent, IDragHandler, IBeginDragHandler
{
    public RotationalDirection rotationalDirection;
    public bool rotationIsInverted;
    protected Vector3 rotationalPlane => GetRotationalPlane();

    public float value => GetValue();
    protected float rotation;
    protected Vector2 prePosition, postPosition;

    public void Start()
    {
        AGS.Debug.RaycasterCheck();
    }
    public virtual void OnBeginDrag(PointerEventData eventData) 
    {
        prePosition = eventData.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        base.HandleInteraction();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        base.HandleInteraction();
    }

    Vector3 GetRotationalPlane()
    {
        Vector3 result = new Vector3();
        int normalize = 1;

        if (rotationIsInverted)
        {
            normalize = -1;
        }
        
        switch (rotationalDirection)
        {
            case RotationalDirection.Forward:
                result = Vector3.forward * normalize;//1,0,0
                break;
            case RotationalDirection.Right:
                result = Vector3.right * normalize;//0,1,0
                break;
            case RotationalDirection.Up:
                result = Vector3.up * normalize;//0,0,1
                break;
        }
        return result;
    }
    protected abstract float GetValue();

    public enum RotationalDirection { Forward, Right, Up }
}