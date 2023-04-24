using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MathExtensions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class ShootScript : MonoBehaviour
{
    #region Timer Related
    [Header("Time Variables")]

    [Tooltip("The time remaining")]
    [HideInInspector] public float timeRemaining = 0f;

    [Tooltip("The time between shots in seconds")]
    [SerializeField]
    private float timeBetweenShots = 1f;

    [Tooltip("The Cooldown reticle hud")]
    [SerializeField]
    private Image cooldownReticle;

    [HideInInspector]
    public bool isLaunching = false;

    private bool isFiredBool = false;

    /// <summary>
    /// Updates the timeRemaining varaible.
    ///     Subtracts delta time from the timer.
    /// </summary>
    /// <param name="deltaTime"></param>
    ///     The change in time between frames
    private void UpdateTimer(float deltaTime)
    {
        timeRemaining -= deltaTime;
    }

    /// <summary>
    /// Checks if the timer is up
    /// </summary>
    /// <returns></returns>
    ///     Returns true if the timer is less than or equal to zero
    public bool IsTimerUp()
    {
        return timeRemaining <= 0f;
    }

    private void ResetTimer()
    {
        timeRemaining = timeBetweenShots;
    }
    #endregion

    #region Bolt related
    [Header("Bolt variables")]

    [Tooltip("Used to store the current bolt index")]
    [SerializeField]
    public int currentBoltIndex;

    [Tooltip("The minimum bolt index (leave be)")]
    private int minBoltIndex = 1;

    [Tooltip("The allowed bolts")]
    [SerializeField]
    private List<BoltTemplate> _allowedBolts;

    [Tooltip("The default bolt if one doesn't exist")]
    [SerializeField]
    private BoltTemplate defaultBolt;

    [Tooltip("Sets the Game Obejct to have all the bolts")]
    [SerializeField]
    private bool isDeveloperMode;

    [SerializeField]
    private AudioClip shootSFX;

    [HideInInspector]
    public CrossbowFireAnim fireAnimScript;

    /// <summary>
    /// Generate ammo wanted by the player
    /// </summary>
    /// <returns></returns>
    ///     An ammo the player selected
    private GameObject GenerateAmmo()
    {
        //Get direction facing
        Vector3 directionVector = PlayerStatic.LookingDirectionVector;
        GameObject ammo = GetSelectedBolt();

        //Instantiate the object
        GameObject spawnedAmmo = ObjectPooling.Spawn(ammo,
            this.gameObject.transform.position + (directionVector * _spawnRange),
            Camera.main.transform.rotation);

        //Return the ammo spawned
        return spawnedAmmo;
    }
    #endregion

    #region Instantiation Variables
    [Header("Instantiation Data Variables")]

    [Tooltip("Instantiate the object x meters away fromt the player")]
    [SerializeField]
    private float _spawnRange;

    [Tooltip("The overarching game data accessing the game data")]
    [SerializeField]
    private OverarchingGameData_SO overarchingGameData;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SetAllowedBolts(_allowedBolts);
    }
    private void Update()
    {
        //Update the timer
        UpdateTimer(Time.deltaTime);

        //Displays the timer on the hud
        //DisplayTimer(timeRemaining);

        if (isLaunching)
        {
            if (currentBoltIndex == 3)
            {
                DisplayTimer(1f);
            }
            else
            {
                DisplayTimer(timeRemaining);
            }

            if (IsTimerUp())
            {
                if (PlayerStatic.IsGrounded)
                {
                    isLaunching = false;
                }
            }
        }
        else
        {
            DisplayTimer(timeRemaining);
        }
    }
    #endregion

    #region Action Methods
    /// <summary>
    /// Used to get a bullet somewhere and fire it in the direction of the player
    /// </summary>
    /// <param name="context"></param>
    ///     The context of the input
    public void Fire(InputAction.CallbackContext context)
    {
        fireAnimScript.PlayFireAnim();

        //Checks if the bolt timer is up
        if (!IsTimerUp())
        {
            return;
        }

        if (isLaunching && currentBoltIndex == 3)
        {
            return;
        }

        //Fire if pressed
        if (context.started)
        {
            //Get the ammo prefab
            GameObject ammo = GenerateAmmo();

            //Execute the on fire method
            ammo.GetComponent<BoltTemplate>().OnFire(this.gameObject, PlayerStatic.LookingDirectionVector);

            // Plays an audio clip
            if (SettingsMenu.instance != null)
                AudioManager.instance.PlaySFX(shootSFX);

            //Reset the time
            ResetTimer();

            if (currentBoltIndex == 3)
            {
                isLaunching = true;
            }
        }

    }

    /// <summary>
    /// Set the allowed bolts by an outside script
    /// </summary>
    /// <param name="allowedBoltObjects"></param>
    ///     Set the outside allowed script to this parameter
    public void SetAllowedBolts(List<BoltTemplate> allowedBoltObjects)
    {
        //Check if there are any bolts inside thing
        if (_allowedBolts.Count > 0)
            _allowedBolts = allowedBoltObjects;
        else
        {
            if (!isDeveloperMode)
            {
                _allowedBolts.Add(defaultBolt);
            }
                
            else
            {
                foreach (var bolt in overarchingGameData.boltTemplates)
                {
                    _allowedBolts.Add(bolt);
                }
            }
        }
        _allowedBolts.TrimExcess();
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
            int maxIndex = _allowedBolts.Count;
            int minIndex = minBoltIndex;

            //Used to keep the integers in a loop
            MathFExtended.Ranges.IntLoopInRange(minIndex, ref newBoltIndex, maxIndex);

            //Assign the bolt index to the script variable
            currentBoltIndex = newBoltIndex;

            fireAnimScript.SetBoltModel();
        }
    }

    public void SetBolt(int newIndex)
    {
        //int newIndex = 1;
        if (newIndex <= _allowedBolts.Count)
        {
            currentBoltIndex = (newIndex);
            fireAnimScript.SetBoltModel();
        }
    }

    /// <summary>
    /// Display the shooting cooldown on the hud
    /// </summary>
    /// <param name="cooldown"></param>
    ///     The time remaining until the next shot
    public void DisplayTimer(float cooldown)
    {
        // If statement checks if cooldown is above 0 (in testing I found it goes into negatives)
        //if (cooldown < -1f)
        if (cooldown < -0.05f)
        {
            cooldownReticle.enabled = false;

            isFiredBool = false;
        }
        // Displays reticle if cooldown is above 0
        else
        {
            cooldownReticle.enabled = true;

            isFiredBool = true;

            // The fill amount is the current rotation of the cooldown reticle
            // this is a ratio of the current cooldown divided by the default time between shots
            // this isn't necessary right now since the default time is one but just in case we change that.
            cooldownReticle.fillAmount = cooldown / timeBetweenShots;
        }
    }
    #endregion

    #region Public bolt files
    /// <summary>
    /// Get the current bolt selected
    /// </summary>
    /// <returns></returns>
    ///     The bolt selected
    public GameObject GetSelectedBolt()
    {
        return _allowedBolts[currentBoltIndex - 1].gameObject;
    }

    /// <summary>
    /// Get's the default bolt
    /// </summary>
    public GameObject GetDefaultBolt()
    {
        return _allowedBolts[0].gameObject;
    }

    public bool isFired()
    {
        return isFiredBool;
    }
    #endregion
}
