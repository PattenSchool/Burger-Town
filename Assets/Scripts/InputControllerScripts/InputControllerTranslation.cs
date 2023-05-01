using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class InputControllerTranslation : StandaloneInputModule
{
    #region Controller Mouse
    [Header("Mouse Vector Variables")]

    [Tooltip("The ratio of the screen")]
    [SerializeField]
    private Vector2 screenRatio = Vector2.one;

    [Tooltip("The speed at which the controller curse goes")]
    [SerializeField]
    private float speed = 1f;

    [Tooltip("The joystick reading")]
    [SerializeField]
    private Vector2 lastKnownJoystickRotation = Vector2.zero;

    /// <summary>
    /// Moves the mouse with the 
    /// </summary>
    public void MoveMouse(InputAction.CallbackContext cxt)
    {

        if (cxt.performed)
            lastKnownJoystickRotation = cxt.ReadValue<Vector2>();
    }

    public void OnVirtualClick(InputAction.CallbackContext cxt)
    {
        Touch newTouch = new()
        {
            position = Mouse.current.position.ReadValue()
        };
        bool isPressed = cxt.performed;
        bool isReleased = cxt.canceled;

        var buttonEvent = GetTouchPointerEventData(newTouch, out isPressed, out isReleased);

        ProcessTouchPress(buttonEvent, isPressed, isReleased);
    }

    private void UpdateMousePosition()
    {
        Vector2 movement = screenRatio * speed * lastKnownJoystickRotation;
        Mouse.current.WarpCursorPosition(Mouse.current.position.ReadValue() + movement);
        print(movement);
    }
    #endregion

    #region Input Variables

    #endregion

    #region Unity Methods
    private void Awake()
    {
    }
    private void Update()
    {
        UpdateMousePosition();
    }
    #endregion
}
