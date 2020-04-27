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
                result = transform.forward * normalize;
                break;
            case RotationalDirection.Right:
                result = transform.right * normalize;
                break;
            case RotationalDirection.Up:
                result = transform.up * normalize;
                break;
        }
        return result;
    }
    protected abstract float GetValue();

    public enum RotationalDirection { Forward, Right, Up }
}