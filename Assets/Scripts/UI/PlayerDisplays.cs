using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplays : MonoBehaviour
{
    #region UI Displays
    [Header("UI Displays")]

    [Tooltip("The UI display to display quest stuff and player HUD")]
    [SerializeField]
    public GameObject playerHud;

    [Tooltip("The player's pause menu canvas")]
    [SerializeField]
    public GameObject pauseMenu;

    [Tooltip("The win menu")]
    [SerializeField]
    public GameObject winMenu;

    [Tooltip("THe death menu for when the player dies")]
    [SerializeField]
    public GameObject deathMenu;
    #endregion
}
