using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : MonoBehaviour, IObjectEvent
{
    [Header("The winning canvas")]

    [Tooltip("Winners canvas")]
    private GameObject winCanvas;

    [Tooltip("Pause menu")]
    private PauseManager pauseManager;

    #region Unity Methods
    private void Start()
    {
        winCanvas = PlayerStatic.UIDisplays.winMenu;
        pauseManager = PlayerStatic.Player.GetComponent<PauseManager>();
    }
    #endregion

    public void IOnEventTriggered()
    {
        pauseManager.TogglePauseNoCanvas(true);;
        winCanvas.SetActive(true);
    }
}
