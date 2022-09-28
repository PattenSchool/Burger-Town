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
        //Used to switch the player input
        playerInput.SwitchCurrentActionMap("UI");
        
        TogglePause(true);
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {

        //Switches player input
        playerInput.SwitchCurrentActionMap("Player");

        TogglePause(false);
    }

    public void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            //Used to freeze the game
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            //Resumes game
            Time.timeScale = 1f;
        }

        //Switch canvases
        pauseCanvas.SetActive(isPaused);
        playerHUD.SetActive(!isPaused);
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
