using System.Collections;
using System.Collections.Generic;
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
    //!===========Variables and Properties===========!//

    //!===================Methods====================!//
    /// <summary>
    /// Sets up the display actions
    /// </summary>
    private void DisplayActionsSetUp()
    {
        SwitchMapToUI();
        playerInput.currentActionMap.actions[4].performed += IncrementTextIndex;
        SwitchMapToPlayer();
    }

    /// <summary>
    /// Stops the display connection to the input, as a just in case
    /// </summary>
    private void StopDisplayActionConnection()
    {

    }

    private void SwitchMapToUI()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }

    private void SwitchMapToPlayer()
    {
        playerInput.SwitchCurrentActionMap("Player");
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
            textDisplayScript.IncrementTextIndex();

        print("It worked");
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
        if (PlayerStatic.HasConversation())
            SwitchMapToUI();
        else
            SwitchMapToPlayer();
    }

    /// <summary>
    //  ?Just in case
    /// </summary>
    private void OnDisable()
    {
        //playerInput.actions["UI"].performed-= IncrementTextIndex;
    }

    private void OnDestroy()
    {
        //playerInput.actions["UI"].performed -= IncrementTextIndex;
    }

    private void OnApplicationQuit()
    {
        //playerInput.actions["UI"].performed -= IncrementTextIndex;
    }
    #endregion
}
