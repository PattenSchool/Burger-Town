using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.MathExtensions;

//https://www.youtube.com/watch?v=1LtePgzeqjQ

public class rbCharacterController : MonoBehaviour
{
    #region Variables
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

    public Vector3 boltVelocity;

    public Vector3 outsideVel;

    public float horizontalFriction = 1f;
    public float verticalFriction = 47f;
    #endregion

    #region Character Controller Methods
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

    private bool CheckGrounded()
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = this.gameObject.transform.lossyScale * (0.5f) + Vector3.down * 0.1f;
        Vector3 direction = Vector3.down;
        Quaternion rotation = transform.rotation;
        float distance = 1f;

        return Physics.BoxCast(center, halfExtents, direction, rotation, distance);
    }

    //void Move()
    //{
    //    //To move (in tutorial)
    //    Vector3 currentVelocity = rb.velocity;
    //    Vector3 targetVelocity = new Vector3(move.x, 0f, move.y);
    //    targetVelocity *= speed;

    //    //Align direction
    //    targetVelocity = transform.TransformDirection(targetVelocity);

    //    //Calculate forces
    //    Vector3 velocityChange = (targetVelocity - currentVelocity);
    //    velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);

    //    //Limit force
    //    Vector3.ClampMagnitude(velocityChange, maxForce);

    //    rb.AddForce(velocityChange, ForceMode.VelocityChange);
    //}
    #endregion

    #region Unity Methods

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock mouse in center of screen

        speed = defaultSpeed;
    }
    
    private void FixedUpdate()
    {
        //Set the ground state
        SetGrounded(CheckGrounded());

        Vector3 currentVelocity = rb.velocity;

        //To Move
        Vector3 desiredmove = transform.rotation * new Vector3(move.x, 0f, move.y) * speed;

        Vector3 velocityChange = (desiredmove - currentVelocity);

        Vector3.ClampMagnitude(velocityChange, maxForce);

        Vector3 finalForce = new Vector3(velocityChange.x, 0f, velocityChange.z);

        if (boltVelocity.magnitude > 0.25f)
        {
            Vector3 temp = new Vector3(velocityChange.x + boltVelocity.x, boltVelocity.y, velocityChange.z + boltVelocity.z);
            rb.AddForce(temp, ForceMode.VelocityChange);
            boltVelocity = new Vector3(boltVelocity.x * Mathf.Lerp(1, 0, horizontalFriction * Time.fixedDeltaTime)
                , boltVelocity.y - Mathf.Lerp(0, boltVelocity.y, verticalFriction * Time.fixedDeltaTime)
                , boltVelocity.z * Mathf.Lerp(1, 0, horizontalFriction * Time.fixedDeltaTime));
        }
        else
        {
            rb.AddForce(finalForce, ForceMode.VelocityChange);
        }

    }

    IEnumerator ResetBoltVelocity()
    {
        yield return new WaitForSeconds(0.3f);

        boltVelocity = Vector3.zero;

        yield break;
    }

    void LateUpdate()                                                                                         //move camera after rest of scene has been updated
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);                                                                         //turn player on up axis

        lookRotation +=(-look.y * sensitivity);                                                                                               //player looks up and down
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);                                                              //player up and down looking stops at halfway up and down
        main_camera.transform.eulerAngles = new Vector3(lookRotation, 
        main_camera.transform.eulerAngles.y, main_camera.transform.eulerAngles.z);                          //rotate the camera (in world space)
    }
    #endregion
}
