using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DL_FixedRotation : BDC_Dial
{
    public float initialRotation, resetRotation, minimumRotation, maximumRotation;

    public void Start()
    {
        
        InitializeRotation();

        void InitializeRotation()
        {
            transform.Rotate(0, initialRotation, 0);
            rotation += initialRotation;
        }

    }

    //void OnMouseDrag()
    //{
    //    Debug.Log("Dragging");
    //}
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //prePosition = eventData.position;
        Debug.Log("OnBeginDrag:2");
    }
    public override void OnDrag(PointerEventData eventData)
    {
        HandleOnDrag();
        base.OnDrag(eventData);

        void HandleOnDrag()
        {
            float change;

            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                change = eventData.delta.x;
            }
            else
            {
                change = eventData.delta.y;
            }

            if ((minimumRotation == 0 && maximumRotation == 0 || rotation + change >= minimumRotation && rotation + change <= maximumRotation))
            {
                transform.Rotate(0, change, 0);
                rotation += change;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    protected override void OnDoubleClick()
    {
        ResetRotation();
    }

    public void ResetRotation()
    {
        transform.rotation = rotationOnStart;
        transform.Rotate(0, resetRotation, 0);
        rotation += resetRotation;
    }
}
