using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MathExtensions;
using UnityEngine.InputSystem;

public class SchootScript : MonoBehaviour
{
    #region Timer Variables
    [Header("Time Variables")]

    [Tooltip("The time remaining")]
    private float timeRemining = 0f;

    [Tooltip("The time between shots in seconds")]
    [SerializeField]
    private float timeBetweenShots = 1f;
    #endregion

    #region Game Variables
    [Header("Game data variables")]

    [Tooltip("The object being shot," +
        "\nRequires a rigid body to work")]
    [SerializeField]
    private GameObject _ammo = null;

    [Tooltip("The player game object")]
    private GameObject player = null;
    #endregion

    #region Instantiation Variables
    [Header("Instantiation Data Variables")]

    [Tooltip("Instantiate the object x meters away fromt the player")]
    [SerializeField]
    private float _spawnRange;
    #endregion

    #region Test Bolt Variables
    [Header("Test Bolt Variables")]

    [Tooltip("The velocity of the bolt")]
    [SerializeField, Min(0f)]
    private float _initialVelocity = 0f;
    #endregion

    #region Bolt Data Variables
    [Header("Bolt variables")]

    [Tooltip("Used to store the current bolt index")]
    [SerializeField]
    public int currentBoltIndex;

    [Tooltip("Different type of bolt prefabs")]
    [SerializeField]
    public BoltTemplate[] boltTemplates;

    [Tooltip("The current level/ the current unlock of the bolt")]
    [SerializeField, Range(1, 11)]
    public int maxUnlockedBoltIndex = 1;

    [Tooltip("The minimum bolt index (leave be)")]
    private int minBoltIndex = 1;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        //Set a player reference
        if (player == null)
        {
            player = this.gameObject;
        }

        
    }

    private void Update()
    {
        //Time functions
        timeRemining -= Time.deltaTime;
    }
    #endregion

    #region Shoot Methods
    /// <summary>
    /// Used to get a bullet somewhere and fire it in the direction of the player
    /// </summary>
    /// <param name="context"></param>
    ///     The context of the input
    public void Fire(InputAction.CallbackContext context)
    {
        if (!(timeRemining <= 0f))
        {
            return;
        }

        //Fire if pressed
        if (context.started)
        {
            //Get direction facing
            Vector3 directionVector = Camera.main.transform.forward;

            //Instantiate the object
            GameObject spawnedAmmo = Instantiate(_ammo);

            spawnedAmmo.transform.position = this.gameObject.transform.position +
                (directionVector * _spawnRange);

            //Add Velocity
            spawnedAmmo.GetComponent<Rigidbody>().velocity = directionVector * _initialVelocity;

            //Add rotation
            spawnedAmmo.transform.eulerAngles = CalcFacingAngles();

            GetComponent<Rigidbody>().AddForce(-directionVector * 100f, ForceMode.Impulse);

            //spawnedAmmo.GetComponent<BoltTemplate>().OnLaunched(player);

            //Reset the time
            timeRemining = timeBetweenShots;
        }

    }
    #endregion

    #region Calculation Methods
    /// <summary>
    /// Get's the normalized vector components of the player 
    /// </summary>
    /// <returns></returns>
    private Vector3 CalcFacingDirection()
    {
        Vector3 rotationVector = CalcFacingAngles();
        float xRotation = rotationVector.x * -1;
        float yRotation = rotationVector.y;

        //Convert to radians
        yRotation = MathFExtended.RotationAndVectorMethods.ConvertToRadians(yRotation);
        xRotation = MathFExtended.RotationAndVectorMethods.ConvertToRadians(xRotation);

        Vector3 normalizedFacingDirection = new Vector3(
            Mathf.Cos(xRotation) * Mathf.Sin(yRotation),
            Mathf.Sin(xRotation),
            Mathf.Cos(xRotation) * Mathf.Cos(yRotation));

        return normalizedFacingDirection;
    }

    /// <summary>
    /// Return the rotation of the direction being faced
    /// </summary>
    /// <returns></returns>
    ///     The direction of the facing direction
    private Vector3 CalcFacingAngles()
    {
        //Get direction currently facing
        float yRotation = player.transform.rotation.eulerAngles.y;
        float xRotation = Camera.main.transform.rotation.eulerAngles.x;

        //Return
        return new Vector3(xRotation, yRotation, 0f);

    }
    #endregion

    #region Bolt Select
    /// <summary>
    /// Used to switch the bolt index
    /// </summary>
    /// <param name="cxt"></param>
    ///     The index of the callback
    public void SwitchBolt(InputAction.CallbackContext cxt)
    {
        //Ensure that the change is only once
        if (cxt.performed)
        {
            //Set to the old bolt index to be modified
            int newBoltIndex = currentBoltIndex;

            //Get only the sign of the number
            newBoltIndex += MathFExtended.SignEx(cxt.ReadValue<Vector2>().y);

            //The level ranges
            int maxIndex = maxUnlockedBoltIndex;
            int minIndex = minBoltIndex;

            //Used to loop around to the other allowed level bolt
            //Such as, currLevel = 1 and index goes down,
            //Then the level is set to max level. 
            //if (newBoltIndex < minIndex)
            //{
            //    newBoltIndex = maxIndex;
            //}
            //else if (newBoltIndex > maxIndex)
            //{
            //    newBoltIndex = minIndex;
            //}
            MathFExtended.Ranges.IntLoopInRange(minIndex, ref newBoltIndex, maxIndex);

            //Assign the bolt index to the script variable
            currentBoltIndex = newBoltIndex;
        }   
    }
    #endregion
}
