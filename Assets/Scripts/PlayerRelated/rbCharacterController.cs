using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
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
    //private float lookRotation;
    public Camera main_camera;
    public bool grounded;
    public float jumpForce;

    public bool isLaunchedByCannon = false;

    //A safe gaurd to expect durring the sprint and jump bug
    public bool isSprinting = false;

    public Vector3 boltVelocity;

    public Vector3 outsideVel;

    public float horizontalFriction = 1f;
    public float verticalFriction = 47f;

    public bool isDebug = false;
    private float debugSpeed = 0f;

    private ShootScript _shootScript;

    private Vector3 lookRotation = Vector3.zero;

    private Controller _con = new Controller();

    #endregion

    //private void OnEnable()
    //{
    //    _con = new Controller();
    //    _con.Enable();
    //    _con.Player.Jump.performed += OnJump;
    //    _con.Player.Sprint.started += OnSprint;
    //    _con.Player.Sprint.canceled += OnSprint;
    //}


    //private void OnDisable()
    //{
    //    _con.Disable();
    //    _con.Player.Jump.performed -= OnJump;
    //    _con.Player.Sprint.started -= OnSprint;
    //    _con.Player.Sprint.canceled -= OnSprint;
    //}

    #region Character Controller Methods
    public void OnMove(InputAction.CallbackContext context) //input system for movement
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpForces = Vector3.zero;
        if (grounded && context.performed)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        //Forced jump
        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
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

    public void ApplySprint()
    {
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }
    }
    #endregion

    #region Unity Methods

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock mouse in center of screen

        speed = defaultSpeed;

        _shootScript = this.gameObject.GetComponent<ShootScript>();
    }

    private void FixedUpdate()
    {
        if (isDebug)
        {
            debugSpeed = 1f;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            debugSpeed = 0f;

            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<CapsuleCollider>().enabled = true;
        }

        //Set the ground state
        SetGrounded(PlayerStatic.IsGrounded);

        Vector3 currentVelocity = rb.velocity;

        ApplySprint();

        //To Move
        Vector3 desiredmove = transform.rotation * new Vector3(move.x, 0f, move.y) * speed;

        Vector3 velocityChange = (desiredmove - currentVelocity);

        Vector3.ClampMagnitude(velocityChange, maxForce);

        Vector3 finalForce = new Vector3(velocityChange.x, velocityChange.y * debugSpeed, velocityChange.z);

        if (boltVelocity.magnitude > 0.25f)
        {
            Vector3 temp = new Vector3(velocityChange.x + boltVelocity.x, boltVelocity.y, velocityChange.z + boltVelocity.z);
            rb.AddForce(temp, ForceMode.VelocityChange);
            boltVelocity = new Vector3(boltVelocity.x * Mathf.Lerp(1, 0, horizontalFriction * Time.fixedDeltaTime)
                , boltVelocity.y - Mathf.Lerp(0, boltVelocity.y, verticalFriction * Time.fixedDeltaTime)
                , boltVelocity.z * Mathf.Lerp(1, 0, horizontalFriction * Time.fixedDeltaTime));

            if (grounded && _shootScript.IsTimerUp())
            {
                boltVelocity = Vector3.zero;
            }
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

    void LateUpdate()                                                                                         
    {
        lookRotation.x += (-look.y * sensitivity);
        lookRotation.x = Mathf.Clamp(lookRotation.x, -90, 90);

        lookRotation.y += (look.x * sensitivity);

        //Camera.main.transform.eulerAngles = lookRotation;

        Camera.main.transform.eulerAngles = new Vector3(lookRotation.x, this.transform.eulerAngles.y
            , lookRotation.z);

        this.transform.eulerAngles = new Vector3(lookRotation.x * debugSpeed, lookRotation.y
            , lookRotation.z);
    }

    public void ToggleDebug()
    {
        if (isDebug)
        {
            isDebug = false;
        }
        else
        {
            isDebug = true;
        }
    }
    #endregion
}
