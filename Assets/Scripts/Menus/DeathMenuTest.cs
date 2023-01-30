using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathMenuTest : MonoBehaviour
{
    [HideInInspector]
    public PlayerInput playerInput;

    private GameObject deathCanvas;
    private GameObject playerHUD;

    public bool isdead;

    public void Start()
    {
        isdead = false;
        Time.timeScale = 1f;

        deathCanvas = PlayerStatic.UIDisplays.deathMenu;
        playerHUD = PlayerStatic.UIDisplays.playerHud;

        playerInput = PlayerStatic.ControllerInput;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == PlayerStatic.PlayerTag)
        {
            isdead = true;
        }

        if(isdead == true)
        {
            playerHUD.SetActive(false);
            deathCanvas.SetActive(true);
            Time.timeScale = 0f;
            playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}