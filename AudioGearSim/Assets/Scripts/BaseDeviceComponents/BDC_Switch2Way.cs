using UnityEngine;

public class BDC_Switch2Way : BDC_Switch
{
    
    public State state { get; private set; }

    public void SetPositionUp()
    {
        animator.SetBool("isUp", true);
        state = State.Up;
    }

    public void SetPositionDown()
    {
        animator.SetBool("isUp", false);
        state = State.Down;
    }

    public void TogglePosition()
    {
        switch (state)
        {
            case State.Down:
                SetPositionUp();
                break;
            case State.Up:
                SetPositionDown();
                break;
        }
    }

    private void OnMouseDown()
    {
        TogglePosition();
        HandleInteraction();
    }

    public enum State { Down, Up }
}
