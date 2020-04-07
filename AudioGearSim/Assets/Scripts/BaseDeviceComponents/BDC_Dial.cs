using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public abstract class BDC_Dial : BaseDeviceComponent, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public float value { get; protected set; }
    protected float rotation;
    protected Vector2 prePosition, postPosition;

    public virtual void OnBeginDrag(PointerEventData eventData) 
    {
        prePosition = eventData.position;
    }

    public abstract void OnDrag(PointerEventData eventData);

    public abstract void OnEndDrag(PointerEventData eventData);
}
