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
    private PlayerInput playerInput;

    //The canvases used in display
    private GameObject pauseCanvas;
    private GameObject playerHUD;

    #region Unity Methods
    private void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        pauseCanvas = PlayerStatic.UIDisplays.pauseMenu;
        playerHUD = PlayerStatic.UIDisplays.playerHud;

        playerInput = PlayerStatic.ControllerInput;
    }
    #endregion


    public void OnPause(InputAction.CallbackContext context)
    {
        //Used to freeze the game
        Time.timeScale = 0f;

        //Used to switch the player input
        playerInput.SwitchCurrentActionMap("UI");
        TogglePause(true);
    }
    public void OnPause()
    {
        //Used to freeze the game
        Time.timeScale = 0f;

        //Used to switch the player input
        playerInput.SwitchCurrentActionMap("UI");
        TogglePause(true);
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {
        //Resumes game
        Time.timeScale = 1f;

        //Switches player input
        playerInput.SwitchCurrentActionMap("Player");

        TogglePause(false);
    }

    public void OnUnPause()
    {
        //Resumes game
        Time.timeScale = 1f;

        //Switches player input
        playerInput.SwitchCurrentActionMap("Player");

        TogglePause(false);
    }

    public void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1f;
        }

        //Switch canvases
        pauseCanvas.SetActive(isPaused);
        playerHUD.SetActive(!isPaused);
    }

    public void TogglePauseNoCanvas(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            playerInput.SwitchCurrentActionMap("UI");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1f;
        }

        //Switch canvases
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
