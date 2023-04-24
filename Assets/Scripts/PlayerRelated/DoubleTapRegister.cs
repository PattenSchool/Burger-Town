using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DoubleTapRegister : MonoBehaviour
{
    #region Double Tap Bool
    [Header("Double Tap Check")]

    [Tooltip("The confirmation if double tap has been achieved")]
    [SerializeField]
    private bool isDoubleTap = false;

    /// <summary>
    /// An accessor checking if a double tap is held
    /// </summary>
    /// <returns></returns>
    public bool GetDoubleTapCheck()
    {
        return isDoubleTap;
    }
    
    /// <summary>
    /// Update the double tap check. The core method of all of this script.
    ///     if a tap is helf and 2 taps are registered, then set true
    /// </summary>
    private void UpdateDoubleTapCheck()
    {
        bool isDoubleTapStillHeld = isTapHeld && tapsRegistered > 1;

        isDoubleTap = isDoubleTapStillHeld;
    }
    #endregion

    #region tap registration variables
    [Header("Tap registers")]

    [Tooltip("The number of taps being registered")]
    [SerializeField]
    private int tapsRegistered = 0;

    [Tooltip("A bool to tell if the last tap is being held")]
    [SerializeField]
    private bool isTapHeld = false;

    [Tooltip("Asking if the timer is still counting down")]
    [SerializeField]
    private bool isTimerCounting = false;

    [Tooltip("A bool determining if the double tap registered")]
    [SerializeField]
    private bool isDoubleTapRegistered = false;

    /// <summary>
    /// Resets the tap count if there
    /// </summary>
    private void UpdateTapReset()
    {
        if (isTimerCounting == false && isTapHeld == false)
            tapsRegistered = 0;
    }
    #endregion

    #region Timer Variables
    [Header("Timer Variables")]

    [Tooltip("How much time until taps are stopped registered from initial tap " +
        "(in seconds)")]
    [SerializeField, Min(0f)]
    private float timerTime = 0f;

    [Tooltip("The timer remaining to register taps in seconds")]
    [SerializeField, Min(0f), HideInInspector]
    private float timeRemaining = 0f;

    /// <summary>
    /// Starts the timer via method to make ease of altering easier
    /// </summary>
    private void StartTimer()
    {
        timeRemaining = timerTime;
        isTimerCounting = true;
    }

    /// <summary>
    /// Update throuhg method rather than varable for ease of updating
    ///     in case of IEnumerator change
    /// </summary>
    /// <param name="deltaTime"></param>
    private void UpdateTimer(float deltaTime)
    {
        timeRemaining -= deltaTime;
        if (timeRemaining <= 0f)
        {
            isTimerCounting = false;
        }
            
    }
    #endregion

    #region input methods
    /// <summary>
    /// Register a tap from a tap on an input (button specific
    /// </summary>
    /// <param name="cxt"></param>
    public void RegisterTap(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            tapsRegistered++;
            if (tapsRegistered == 1)
            {
                StartTimer();
            }
        }
    }

    /// <summary>
    /// Register if a hold is being held
    /// </summary>
    /// <param name="cxt"></param>
    public void RegisterHold(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            isTapHeld = true;
        }
        else if (cxt.canceled)
        {
            isTapHeld = false;
        }
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        //Updates timer
        UpdateTimer(Time.deltaTime);

        //Update Held condition for reset of tap counter
        UpdateTapReset();

        //Update method on if double tap is achieved
        UpdateDoubleTapCheck();
    }
    #endregion
}
