using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Deals with the communication with the player dialogue and input
/// </summary>
[RequireComponent(typeof(MainTextDisplay))]
public class DialogueInputManager : MonoBehaviour
{
    #region Input Components
    //!===========Variables and Properties===========!//
    [Header("Input Components")]

    [Tooltip("The input connected to the player")]
    [SerializeField]
    private PlayerInput playerInput;

    [Tooltip("THe main text display to get the player's text index")]
    [SerializeField]
    private MainTextDisplay textDisplayScript;
    #endregion

    #region Input Sub Parts
    //!===================Methods====================!//
    /// <summary>
    /// Sets up the display actions
    /// </summary>
    private void DisplayActionsSetUp()
    {
        SwitchMapToUI();
        playerInput.currentActionMap.actions[4].started += IncrementTextIndex;
        SwitchMapToPlayer();
    }

    /// <summary>
    /// Stops the display connection to the input, as a just in case
    /// </summary>
    private void StopDisplayActionConnection()
    {
        //SwitchMapToUI();
        //playerInput.currentActionMap.actions[4].started -= IncrementTextIndex;
        //SwitchMapToPlayer();
    }

    /// <summary>
    /// Switches the player map to "UI"
    /// </summary>
    private void SwitchMapToUI()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }

    /// <summary>
    /// Switches the player map to the default "Player" controller
    /// </summary>
    private void SwitchMapToPlayer()
    {
        playerInput.SwitchCurrentActionMap(playerInput.defaultActionMap);
    }
    #endregion

    #region Proxy Increment Method
    //!===================Methods====================!//
    /// <summary>
    /// INcrement text if it exists
    /// </summary>
    /// <param name="cxt"></param>
    private void IncrementTextIndex(InputAction.CallbackContext cxt)
    {
        if (PlayerStatic.HasConversation())
           StartCoroutine(IncrementTextIndexTimed());
    }

    private IEnumerator IncrementTextIndexTimed()
    {
        //wait for a small amount of time
        yield return new WaitForSeconds(0.1f);

        //Increment the text display
        textDisplayScript.IncrementDialogueIndex();

        //Cancel any remaining text displays
        StopAllCoroutines();
    }
    #endregion

    #region Unity Methods
    //!===================Methods====================!//
    private void Start()
    {
        //Initialize player stuff
        playerInput = PlayerStatic.ControllerInput;
        textDisplayScript = this.GetComponent<MainTextDisplay>();

        //Set event to this 
        DisplayActionsSetUp();
    }

    private void Update()
    {
        SwitchMap();
    }

    /// <summary>
    //  ?Just in case
    /// </summary>
    private void OnDisable()
    {
        StopDisplayActionConnection();
    }

    /// <summary>
    //  ?Just in case
    /// </summary>
    private void OnApplicationQuit()
    {
        StopDisplayActionConnection();
    }

    /// <summary>
    //  ?Just in case
    /// </summary>
    private void OnDestroy()
    {
        StopDisplayActionConnection();
    }

    private void OnEnable()
    {
        DisplayActionsSetUp();
    }
    #endregion

    #region Switch Maps
    public void SwitchMap()
    {
        if (PlayerStatic.HasConversation())
            SwitchMapToUI();
        else if (playerInput.currentActionMap.name != playerInput.defaultActionMap
            && !PlayerStatic.HasConversation())
            SwitchMapToPlayer();
    }
    #endregion
}