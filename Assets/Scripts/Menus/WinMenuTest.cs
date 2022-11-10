using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WinMenuTest : MonoBehaviour
{
    public PlayerInput playerInput;

    private GameObject winCanvas;
    private GameObject playerHUD;

    #region Unity Methods
    private void Start()
    {
        winCanvas = PlayerStatic.UIDisplays.winMenu;
        playerHUD = PlayerStatic.UIDisplays.playerHud;
    }
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            playerHUD.SetActive(false);
            winCanvas.SetActive(true);
            Time.timeScale = 0f;
            playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
