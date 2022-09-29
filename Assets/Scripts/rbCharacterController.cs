using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//https://www.youtube.com/watch?v=1LtePgzeqjQ

public class rbCharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public float defaultSpeed;
    private float speed;
    public float sprintSpeed;
    public float sensitivity;
    public float maxForce;
    private Vector2 move;
    private Vector2 look;
    private float lookRotation;
    public Camera main_camera;
    public bool grounded;
    public float jumpForce;


    public void OnMove(InputAction.CallbackContext context) //input system for movement
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpForces = Vector3.zero;
        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = sprintSpeed;
        }
        else if (context.canceled)
        {
            speed = defaultSpeed;
        }
    }

    public void SetGrounded(bool state)
    {
        grounded = state;
    }

    public void OnLook(InputAction.CallbackContext context) //input system for rotation
    {
        look = context.ReadValue<Vector2>();
    }

    private void FixedUpdate() //use fixed because we have a rb that is physics-based
    {
        
        Vector3 currentVelocity = rb.velocity; //find target velocity
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y); //take input and change it into a vector to move character
        targetVelocity *= speed;

        targetVelocity = transform.TransformDirection(targetVelocity); //align direction with player so we move in right direction

        Vector3 velocityChange = (targetVelocity - currentVelocity); //calculate amount of forces to apply to player
        
        // Adds gravity to player by setting vertical velocity to zero
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        Vector3.ClampMagnitude(velocityChange, maxForce); //limit amount of force on player

        rb.AddForce(velocityChange, ForceMode.VelocityChange); //add velocity change to player
        
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock mouse in center of screen

        speed = defaultSpeed;
    }

    private void Update()
    {
        SetGrounded(CheckGrounded());
    }

    void LateUpdate()                                                                                         //move camera after rest of scene has been updated
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);                                                                         //turn player on up axis

        lookRotation +=(-look.y * sensitivity);                                                                                               //player looks up and down
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);                                                              //player up and down looking stops at halfway up and down
        main_camera.transform.eulerAngles = new Vector3(lookRotation, 
        main_camera.transform.eulerAngles.y, main_camera.transform.eulerAngles.z);                          //rotate the camera (in world space)
    }

    private bool CheckGrounded()
    {
        Vector3 center = this.gameObject.transform.position - 
            (this.gameObject.transform.lossyScale.y * Vector3.down * 0.5f) - new Vector3(0f, 0.1f/2f, 0f);
        Vector3 halfExtents = this.gameObject.transform.lossyScale * (0.5f) + Vector3.down * 0.1f;
        Vector3 direction = Vector3.down;

        return Physics.BoxCast(center, halfExtents, direction);
    }

}
