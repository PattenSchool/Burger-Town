using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Used to organize pause menu functionality
/// </summary>
public class PauseManager : MonoBehaviour
{
    //Used to get the player input manager
    public PlayerInput playerInput;

    //The canvases used in display
    public GameObject pauseCanvas;
    public GameObject playerHUD;

    public void OnPause(InputAction.CallbackContext context)
    {
        //Used to freeze the game
        Time.timeScale = 0f;

        //Used to switch the player input
        playerInput.SwitchCurrentActionMap("UI");

        //Used to unlock the cursor from the screen
        Cursor.lockState = CursorLockMode.None;

        //Used to switch canvases
        pauseCanvas.SetActive(true);
        playerHUD.SetActive(false);
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {

    }

    public void FreezeTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

}
