using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DL_FixedRotation : BDC_Dial
{
    public float minimumRotation, maximumRotation;

    public void Start()
    {
        InitializeRotation();

        void InitializeRotation()
        { 
            
        }

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
                transform.Rotate(0, 0, -change);
                rotation += change;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
}
