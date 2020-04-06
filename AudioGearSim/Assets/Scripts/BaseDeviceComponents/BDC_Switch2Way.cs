using UnityEngine;

public class BDC_Switch2Way : BaseDeviceComponent
{
    Animator animator => GetComponentInChildren<Animator>();
    public State state { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
