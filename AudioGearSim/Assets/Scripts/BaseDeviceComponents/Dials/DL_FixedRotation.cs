using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DL_FixedRotation : BDC_Dial
{
    public float initialRotation, minimumRotation, maximumRotation;
    public 

    new void Start()
    {
        base.Start();
        InitializeRotation();
        //Debug.Log(transform.localRotation.eulerAngles);
        void InitializeRotation()
        {
            SetRotation(initialRotation);
        }

    }

    //void OnMouseDrag() //somehow this works if bounding box obscures
    //{
    //    Debug.Log("Dragging");
    //}
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //prePosition = eventData.position;
        Debug.Log("OnBeginDrag");
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
            SetRotation(rotation + change);
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
        SetRotation(initialRotation);
    }

    void SetRotation(float degrees)
    {
        //transform.eulerAngles = new Vector3();
        if (degrees > maximumRotation)
        {
            degrees = maximumRotation;
        }
        if (degrees < minimumRotation)
        {
            degrees = minimumRotation;
        }

        transform.localEulerAngles =  rotationalPlane*degrees;
        
        rotation = degrees;
    }

    protected override float GetValue()
    {
        return rotation / (maximumRotation - minimumRotation);
    }
}
