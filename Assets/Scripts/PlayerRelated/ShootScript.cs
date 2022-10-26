using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MathExtensions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShootScript : MonoBehaviour
{
    #region Timer elements
    [Header("Time Variables")]

    [Tooltip("The time remaining")]
    private float timeRemaining = 0f;

    [Tooltip("The time between shots in seconds")]
    [SerializeField]
    private float timeBetweenShots = 1f;

    /// <summary>
    /// Updates the time remaining with the time passed
    /// </summary>
    private void UpdateTimer(float deltaTime)
    {
        timeRemaining -= deltaTime;
    }

    /// <summary>
    /// Checks if timer ran out
    /// </summary>
    /// <returns></returns>
    ///     True if time remaining ran out
    private bool IsTimerEnd()
    {
        return timeRemaining <= 0f;
    }
    #endregion

    #region Bolt Variables
    [Header("Instantiation Data Variables")]

    [Tooltip("Instantiate the object x meters away fromt the player")]
    [SerializeField]
    private float _spawnRange;

    [Header("Bolt variables")]

    [Tooltip("Used to store the current bolt index")]
    [SerializeField]
    public int currentBoltIndex;

    [Tooltip("Different type of bolt prefabs")]
    [SerializeField]
    public BoltTemplate[] boltPrefabs;

    [Tooltip("The current level/ the current unlock of the bolt")]
    [SerializeField, Range(1, 11)]
    public int maxUnlockedBoltIndex = 1;

    [Tooltip("The minimum bolt index (leave be)")]
    private int minBoltIndex = 1;

    [Tooltip("The velocity of the bolt")]
    [SerializeField, Min(0f)]
    private float _initialVelocity = 0f;
    #endregion

    #region Bolt Methods
    public GameObject GetSelectedBolt()
    {
        return boltPrefabs[currentBoltIndex - 1].gameObject;
    }
    #endregion

    #region Input Methods
    /// <summary>
    /// Used to get a bullet somewhere and fire it in the direction of the player
    /// </summary>
    /// <param name="context"></param>
    ///     The context of the input
    public void Fire(InputAction.CallbackContext context)
    {
        //Cancel action if timer doesn't exist
        if (!IsTimerEnd())
        {
            return;
        }

        //Fire if pressed
        if (context.started)
        {
            //Get direction facing
            Vector3 directionVector = PlayerStatic.LookingDirectionVector;

            //Get the ammo prefab
            GameObject ammo = boltPrefabs[currentBoltIndex - 1].gameObject;

            //Instantiate the object
            GameObject spawnedAmmo = ObjectPooling.Spawn(ammo,
                transform.position + (directionVector * _spawnRange),
                PlayerStatic.LookingDirection);

<<<<<<< Updated upstream:Assets/Sage's test stuff/Scripts/PlayerRelated/SchootScript.cs
            //Add Velocity
            spawnedAmmo.GetComponent<Rigidbody>().velocity = directionVector * _initialVelocity;

            //Add rotation
            spawnedAmmo.transform.rotation = Camera.main.transform.rotation;

            spawnedAmmo.GetComponent<BoltTemplate>().OnLaunched(player);
=======
            //Execute the on fire method
            spawnedAmmo.GetComponent<BoltTemplate>().OnFire(this.gameObject);
>>>>>>> Stashed changes:Assets/Scripts/PlayerRelated/ShootScript.cs

            //Reset the time
            timeRemaining = timeBetweenShots;
        }

    }

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

            //Used to keep the integers in a loop
            MathFExtended.Ranges.IntLoopInRange(minIndex, ref newBoltIndex, maxIndex);

            //Assign the bolt index to the script variable
            currentBoltIndex = newBoltIndex;
        }
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        //Update timer
        UpdateTimer(Time.deltaTime);
    }
    #endregion
}
