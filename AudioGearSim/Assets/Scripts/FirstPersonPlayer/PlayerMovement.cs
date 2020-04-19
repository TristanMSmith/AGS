using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller => GetComponent<CharacterController>();
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
        //get lateral movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //collision check to reset our velocity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //player moves laterally
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * PlayerSettings.speed * Time.deltaTime);
        
        //player moves vertically (jumps)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(PlayerSettings.jumpHeight * -2f * PlayerSettings.gravity);
        }
        
        //apply gravity to the player
        velocity.y += PlayerSettings.gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
    }
}
