using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : MonoBehaviour, IObjectEvent
{
    [Header("The winning canvas")]

    [Tooltip("Winners canvas")]
    [SerializeField]
    private GameObject winCanvas;

    [Tooltip("Pause menu")]
    [SerializeField]
    private PauseManager pauseManager;

    public void IOnEventTriggered()
    {
        pauseManager.TogglePauseNoCanvas(true);;
        winCanvas.SetActive(true);
    }
}
