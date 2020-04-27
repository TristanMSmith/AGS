using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public abstract class BDC_Dial : BaseDeviceComponent, IDragHandler, IBeginDragHandler
{
    public Vector3 rotationalPlane;
    public float value { get; protected set; }
    protected float rotation;
    protected Vector2 prePosition, postPosition;
    //protected Vector3 rotation;
    protected Quaternion rotationOnStart;

    public void Start()
    {
        rotationOnStart = transform.rotation;
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

    

    public enum RotationalPlane { Up, Down, Left, Right, Forward, Backword }
}