using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathMenuTest : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject deathCanvas;
    public GameObject playerHUD;

    public bool isdead;

    public void Start()
    {
        isdead = false;
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "KillTest")
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