using UnityEngine;
using UnityEngine.InputSystem;

//https://www.youtube.com/watch?v=1LtePgzeqjQ
[RequireComponent(typeof(Rigidbody))]
public class rbCharacterController : MonoBehaviour
{
    #region Component variables
    [Header("Component Variables")]

    [Tooltip("The player Manager")]
    [HideInInspector]
    public Rigidbody rb;
    #endregion

    #region Locomotion
    [Header("Locomotion Variables")]

    [Tooltip("The default speed of the player")]
    [SerializeField, Min(0f)]
    private float defaultSpeed;

    [Tooltip("The sprint speed of the player")]
    [SerializeField, Min(0f)]
    private float sprintSpeed;

    [Tooltip("The current speed of the player")]
    private float speed;

    [Tooltip("Rotation speed")]
    [SerializeField, Min(0f)]
    private float sensitivity;

    [Header("Calculation Locomotion variables")]
    
    [Tooltip("The current move velocity of the player")]
    private Vector2 move;

    [Tooltip("The current change in intended changing rotation")]
    private Vector2 look;

    [Tooltip("The current looking angles of the y angle and make sure that it's in bounds")]
    private float lookRotation;

    /// <summary>
    /// Get's the movement vector from the input system
    /// </summary>
    /// <param name="context"></param>
    ///     The info transfer from the input system to the method
    public void OnMove(InputAction.CallbackContext context) //input system for movement
    {
        move = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
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

    
    /// <summary>
    /// Gets the look vector from the input system
    /// </summary>
    /// <param name="context"></param>
    ///     The context from the input system
    public void OnLook(InputAction.CallbackContext context) //input system for rotation
    {
        look = context.ReadValue<Vector2>();
    }
    #endregion

    #region Jump Components
    [Header("Jump Variables")]

    [Tooltip("A check if the player is grounded")]
    [SerializeField]
    private bool grounded;

    [Tooltip("The force of the jump (how high the player can jump")]
    [SerializeField, Min(0.01f)]
    private float jumpForce;

    /// <summary>
    /// Checks if the player is touching anything
    /// </summary>
    /// <returns></returns>
    ///     A bool if the plaer is touching something
    private bool CheckGrounded()
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = PlayerStatic.Player.transform.lossyScale * (0.5f) + Vector3.down * 0.1f;
        Vector3 direction = Vector3.down;
        Quaternion rotation = transform.rotation;
        float distance = 1f;


        return Physics.BoxCast(center, halfExtents, direction, rotation, distance);
    }

    /// <summary>
    /// Allows the player to jump if told to by the input system
    /// </summary>
    /// <param name="context"></param>
    ///     The context of the input system
    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpForces = Vector3.zero;
        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Justs sets the grounded state
    /// </summary>
    /// <param name="state"></param>
    ///     The state of being grounded
    public void SetGrounded(bool state)
    {
        grounded = state;
    }
    #endregion

    #region Unity Methods
    /// <summary>
    /// Set up the required stuff for the character controller
    /// </summary>
    void Start()
    {
        //lock mouse in center of screen
        Cursor.lockState = CursorLockMode.Locked; 

        //Sets the speed to default
        speed = defaultSpeed;

        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// One update per frame
    /// </summary>
    private void Update()
    {
        //Set the ground state
        SetGrounded(CheckGrounded());

<<<<<<< Updated upstream:Assets/Sage's test stuff/Scripts/PlayerRelated/rbCharacterController.cs
        

        //To Move
        Vector3 desiredmove = transform.rotation * new Vector3(move.x, 0f, move.y) * speed;
        rb.velocity = new Vector3(desiredmove.x, rb.velocity.y, desiredmove.z);
=======
        //To Move
        Vector3 desiredmove = transform.rotation * new Vector3(move.x, 0f, move.y) * speed;

        //If the payer is going faster than normal, then stop movement
        if (rb.velocity.magnitude > desiredmove.magnitude)
        {
            return;
        }

        //Set the players velocity
        rb.velocity = new Vector3(desiredmove.x, 
            rb.velocity.y, desiredmove.z);
>>>>>>> Stashed changes:Assets/Scripts/PlayerRelated/rbCharacterController.cs
    }

    /// <summary>
    /// move camera after rest of scene has been updated
    /// </summary>
    void LateUpdate()
    {
        //turn player on up axis
        transform.Rotate(Vector3.up * look.x * sensitivity);

        //player looks up and down
        lookRotation += (-look.y * sensitivity);
        //player up and down looking stops at halfway up and down
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);

        //rotate the camera (in world space)
        Camera mainCamera = PlayerStatic.MainCamera;
        mainCamera.transform.eulerAngles = new Vector3(lookRotation, 
        mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);                          
    }
    #endregion
}
