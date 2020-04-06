using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public abstract class BD_GuitarPedal : BaseDevice
{
    public BDC_Lead lead { get; private set; }
    Vector3 mOffset;
    float mZCoord;
    public void Start()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.Log("Camera doesn't have a physics raycaster.");
        }
    }
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}