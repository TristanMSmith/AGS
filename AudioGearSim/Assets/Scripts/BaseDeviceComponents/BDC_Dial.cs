using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class BDC_Dial : BaseDeviceComponent
{
    public float minRotation, maxRotation;
    public float speed = 20;
    public int value { get; private set; }
    public void Start()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.Log("Camera doesn't have a physics raycaster.");
        }

        transform.eulerAngles = new Vector3(minRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    void OnMouseDrag()
    {
        float rotY = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.forward, rotY);

        if (transform.eulerAngles.x < minRotation)
        {
            transform.eulerAngles = new Vector3(minRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (transform.eulerAngles.x > maxRotation)
        {
            transform.eulerAngles = new Vector3(maxRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
