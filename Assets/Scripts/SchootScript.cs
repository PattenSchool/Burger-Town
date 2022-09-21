using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MathExtensions;
using UnityEngine.InputSystem;

public class SchootScript : MonoBehaviour
{
    #region Game Variables
    [Header("Game data variables")]

    [Tooltip("The object being shot," +
        "\nRequires a rigid body to work")]
    [SerializeField]
    private GameObject _ammo = null;

    [Tooltip("The player game object")]
    private GameObject player = null;

    [Tooltip("The action to tell to fire")]
    [SerializeField]
    public Controller _input;
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

    #region Unity Methods
    private void OnEnable()
    {
        if (player == null)
        {
            player = this.gameObject;
        }

        if (_input == null)
        {
            _input = new Controller();
        }

        _input.Player.Fire.started += cxt => Fire();

        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }
    #endregion

    #region Shoot Methods
    public void Fire()
    {


        //Get direction facing
        //Vector3 directionVector = CalcFacingDirection();
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
}
