using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BDC_Lead : BaseDeviceComponent
{
    public BDC_Jack jack { get; private set; }
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
    public bool Connect(BDC_Jack Jack)
    {
        if (jack == null)
        {
            jack = Jack;
            return true;
        }
        return false;
    }
    public bool Disconnect()
    {
        if (jack != null)
        {
            jack = null;
            return true;
        }
        return false;        
    }
}