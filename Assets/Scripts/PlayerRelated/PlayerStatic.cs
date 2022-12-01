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
        private set { }
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

    #region Set Up Methods
    /// <summary>
    /// Set up the static player class
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
        textQueue = new Queue<string>();
    }

    private static void SetUpPlayerInput(GameObject player)
    {
        ControllerInput = player.GetComponent<PlayerInput>();
    }
    #endregion

    #region Main Text Elements
    /// <summary>
    /// Used to display the text in the main display
    /// </summary>
    private static Queue<string> textQueue;
    public static Queue<string> TextQueue
    {
        get { return textQueue; }
        private set { textQueue = value; }
    }

    /// <summary>
    /// Adds a new text to the text queue
    /// </summary>
    /// <param name="newText"></param>
    ///     The new text
    public static void AddToTextQueue(string newText)
    {
        textQueue.Enqueue(newText);
    }

    /// <summary>
    /// Adds multiple texts to a queue
    /// </summary>
    /// <param name="newTexts"></param>
    ///     The new texts
    public static void AddTextsToQueue(string[] newTexts)
    {
        foreach (string newText in newTexts)
        {
            AddToTextQueue(newText);
        }
    }
    #endregion

    
    #region Conversation stuff
    private static Conversation_SO conversation;
    public static Conversation_SO Conversation
    {
        get { return conversation; }
    }

    public static void OverrideConversation(Conversation_SO newConversation)
    {
        conversation = newConversation;
    }

    public static bool HasConversation()
    {
        return conversation != null;
    }

    public static void DeleteConversation()
    {
        conversation = null;
    }
    #endregion
        
}
