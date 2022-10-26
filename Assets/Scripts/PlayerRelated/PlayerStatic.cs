using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private static ShootScript _shootScript;

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

    #region Methods
    /// <summary>
    /// et up the static player class
    /// </summary>
    /// <param name="player"></param>
    ///     The player game object
    public static void SetupPlayer(GameObject player)
    {
        Player = player;
        MainCamera = Camera.main;
        SetUpShootScript(player);
    }

    /// <summary>
    /// Sets up the shoot script
    /// </summary>
    private static void SetUpShootScript(GameObject player)
    {
        _shootScript = player.GetComponent<ShootScript>();
    }
    #endregion
}
