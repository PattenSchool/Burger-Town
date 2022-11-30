using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Used to access the player from anywhere in the scene
/// </summary>
public class PlayerStatic
{
    #region Player Components
    /// <summary>
    /// The player game object
    /// </summary>
    public static GameObject Player
    {
        get;
        private set;
    }

    /// <summary>
    /// The main camera's game object
    /// </summary>
    public static Camera MainCamera
    {
        get;
        private set;
    }

    public static PlayerInput ControllerInput
    {
        get;
        private set;
    }
    #endregion

    #region Player Elements
    /// <summary>
    /// The direction the player is looking in quaternion form
    /// Read only
    /// </summary>
    public static Quaternion LookingDirection
    {
        get
        {
            return MainCamera.transform.rotation;
        }
        private set
        {
            //Empty on purpose
        }
    }

    /// <summary>
    /// The direction the player is looking as a vector 
    /// read only
    /// </summary>
    public static Vector3 LookingDirectionVector
    {
        get
        {
            return MainCamera.transform.forward;
        }
        private set
        {
            //Emplty on purpose
        }
    }

    public static string PlayerTag
    {
        get
        {
            return "Player";
        }
    }
    #endregion

    #region Bolt Elements
    /// <summary>
    /// A reference to the shoot script
    /// </summary>
    public static ShootScript _shootScript;

    /// <summary>
    /// Get a selected bolt found from the player
    /// </summary>
    public static GameObject BoltSelected
    {
        get
        {
            return _shootScript.GetSelectedBolt();
        }
    }
    #endregion

    #region UIElements
    /// <summary>
    /// Used to get and handle the player displays
    /// </summary>
    private static PlayerDisplays _uiDisplays;
    public static PlayerDisplays UIDisplays
    {
        get { return _uiDisplays; }
        private set {  }
    }
    #endregion

    #region Error Testing
    /// <summary>
    /// Keeps track of how many players extra are trying to access this script
    /// </summary>
    private static int _nonNeededPlayers = 0;

    /// <summary>
    /// Warns of how many players are remaining for this script to work
    /// </summary>
    /// <returns></returns>
    ///     Message telling the console of how many unnecessary players are remaining
    public static string PlayerWarning()
    {
        return $"Please destroy extra players, {_nonNeededPlayers} remaining";
    }

    /// <summary>
    /// Tells if there are any extra players remaining at all
    /// </summary>
    /// <returns></returns>
    ///     Gives a bool if there are any m ore players than the 1 allowed
    public static bool IsExtraneousPlayers()
    {
        return _nonNeededPlayers > 0;
    }
    #endregion

    #region Methods
    /// <summary>
    /// et up the static player class
    /// </summary>
    /// <param name="player"></param>
    ///     The player game object
    public static void SetupPlayer(GameObject player)
    {
        //Check if there already a player registered, if not, then garantees that there is only one
        if (Player == null)
        {
            Player = player;
        }
        else if (Player != null)
        {
            player.SetActive(false);
            _nonNeededPlayers++;
        }

        MainCamera = Camera.main;
        SetUpShootScript(Player);
        SetUpUIDisplays(Player);
        SetUpPlayerInput(Player);
    }

    /// <summary>
    /// Sets up the shoot script
    /// </summary>
    private static void SetUpShootScript(GameObject player)
    {
        _shootScript = player.GetComponent<ShootScript>();
    }

    private static void SetUpUIDisplays(GameObject player)
    {
        _uiDisplays = player.GetComponent<PlayerDisplays>();
    }

    private static void SetUpPlayerInput(GameObject player)
    {
        ControllerInput = player.GetComponent<PlayerInput>();
    }
    #endregion
}
