using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Used to switch between the player input and the UI input
/// </summary>
public class InputController : MonoBehaviour
{
    public PlayerInput PlayerControllerInput;
    public PlayerInput UIControllerInput;
    public Controller controller;

    private void Update()
    {
        
    }
}
