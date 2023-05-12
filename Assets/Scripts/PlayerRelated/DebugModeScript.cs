using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugModeScript : MonoBehaviour
{
    // Text input field
    public TMP_InputField consoleText;

    //
    public TMP_Text textOutput;
    private List<string> messages = new List<string>();
    public int maximumMessages;

    //
    public GameObject console;

    // Gameobjects that are disabled when debugging
    public MonoBehaviour[] scripts;
    public Animation[] clips;
    public Rigidbody[] rigidBodies;
    public ConsoleCommand[] commands;

    private PlayerInput _playerInput;
    public bool isDebug = false;

    void Start()
    {
        _playerInput = PlayerStatic.ControllerInput;
    }

    public void EnableDebug(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale != 0)
        {
            if (isDebug)
            {
                //consoleText.gameObject.SetActive(false);
                console.gameObject.SetActive(false);

                isDebug = false;
            }
            else
            {
                //consoleText.gameObject.SetActive(true);
                console.gameObject.SetActive(true);

                //StartCoroutine(CheckConsole());

                isDebug = true;
            }
            DisableComponents(isDebug);
        }
    }

    private void DisableComponents(bool debug)
    {
        if (debug)
        {
            Cursor.lockState = CursorLockMode.None;
            _playerInput.SwitchCurrentActionMap("UI");

            consoleText.ActivateInputField();
            consoleText.Select();

            foreach (var script in scripts)
            {
                script.enabled = false;
            }

            foreach (var clip in clips)
            {
                clip.enabled = false;
            }

            foreach (var rb in rigidBodies)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            _playerInput.SwitchCurrentActionMap("Player");

            consoleText.DeactivateInputField();

            foreach (var script in scripts)
            {
                script.enabled = true;
            }

            foreach (var clip in clips)
            {
                clip.enabled = true;
            }

            foreach (var rb in rigidBodies)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }

    public void ReadStringInput(string input)
    {
        // Set timescale to one on end register

        //print(input);
        //PrintToConsole(input);
        RunCommand(input);
        consoleText.text = null;
        consoleText.ActivateInputField();
        consoleText.Select();
    }

    public void RunCommand(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        bool isValid = false;

        foreach (var command in commands)
        {
            if (command.name.ToUpper() == input.ToUpper())
            {
                PrintToConsole(command.Run());
                isValid = true;
            }
            else if (input.ToUpper().Contains(command.name.ToUpper()))
            {
                char[] message;
                message = input.ToCharArray();

                string phrase = "";

                foreach (char character in message)
                {
                    if (char.IsDigit(character))
                    {
                        phrase += character;
                    }
                }

                float parameter = float.Parse(phrase);

                PrintToConsole(command.RunParameter(parameter));
                isValid = true;
            }
        }

        if (isValid)
        {
            PrintToConsole(input);
            isValid = false;
        }
        else
        {
            PrintToConsole("Command not recognized");
        }
    }

    public void PrintToConsole(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        messages.Add(text);

        if (messages.Count > maximumMessages)
        {
            messages.RemoveAt(0);

            textOutput.text = "";

            foreach (string message in messages)
            {
                textOutput.text += "\n" + message;
            }
        }
        else
        {
            textOutput.text += "\n" + text;
        }
    }


    /*
    private IEnumerator CheckConsole()
    {
        while (isDebug)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);

            // function goes here

        }
        yield break;
    }
    */

}
