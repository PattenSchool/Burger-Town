using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;

public static class InputStatic
{
    /// <summary>
    /// The reference to the input names and bindings
    /// </summary>
    public static class InputNames
    {
        #region Input Variables
        [Header("Input names")]

        [Tooltip("The input reference to get the names of the bindings and info")]
        public static Controller inputNames = new Controller();
        #endregion

        #region Binding Methods
        /// <summary>
        /// Get the bindings listings of an action
        /// </summary>
        /// <param name="playerAction"></param>
        ///     The action that is being set 
        /// <param name="controlScheme"></param>
        ///     The control scheme being checked for
        /// <returns></returns>
        public static string GetBindingsStrings(InputAction playerAction, InputControlScheme controlScheme)
        {
            //Defualt test result variable
            string result = "";

            //TODO: Loop through all the bindings
            for (int i = 0; i < playerAction.bindings.Count; i++)
            {
                //TODO: Getting the binding of the controller
                var binding = playerAction.bindings[i];
                
                
                //TODO: Check if the button is composite 
                bool isCompositeCompatible = 
                    binding.isComposite &&
                    playerAction.bindings[i + 1].groups.Contains(controlScheme.bindingGroup.ToString());

                if (isCompositeCompatible)
                {
                    if (result == "")
                        result = binding.name.ToString();
                    else
                        result += $" or {binding.name.ToString()}";
                }

                //TODO: Check if the button is a regular button
                bool isRegularButton = binding.groups != null && !binding.isComposite && 
                    !binding.isPartOfComposite &&
                    binding.groups.Contains(controlScheme.bindingGroup.ToString());

                if (isRegularButton)
                {
                    if (result == "")
                        result = binding.ToDisplayString().ToString();
                    else
                        result += $" or {binding.ToDisplayString().ToString()}";
                }
            }

            //TODO: Return the result
            return result;
        }

        /// <summary>
        /// Get the bindings string by auto checking if a gamepad is connected
        /// </summary>
        /// <param name="playerAction"></param>
        ///     The player action being annalyzed for bindings
        /// <returns></returns>
        public static string GetBindingsStrings(InputAction playerAction)
        {
            if (InputStatic.InputData.IsAGamepadConnected())
            {
                return GetBindingsStrings(playerAction, InputStatic.InputNames.GamepadControlScheme());
            }
            else
            {
                return GetBindingsStrings(playerAction, InputStatic.InputNames.KeyboardControlSchemeTag());
            }
        }

        /// <summary>
        /// Get's a binding based on an action call from a string
        /// </summary>
        /// <param name="actionCall"></param>
        /// <returns></returns>
        public static string GetBindingByCall(string actionCall)
        {
            //TODO: Get safe result
            string result = "";

            //TODO: GO through all defined actions
            foreach(var actionDescription in allActions)
            {
                if (actionCall.ToSafeString().ToUpper() == actionDescription.ActionCallingString.ToUpper())
                {
                    //TODO: Add bindings to the result
                    result =
                        GetBindingsStrings(actionDescription.Action);

                    //TODO: Return the result
                    return result;
                }
            }

            //Set default result in case of not found action
            result = "";

            //Return the default result
            return result;
        }

        public static string TranslateTextToInstruction(string originalText, char commandCharacter)
        {
            string result = "";

            //TODO: split the original text into seperate parts 
            string[] splitText = originalText.Split(commandCharacter);
            if (splitText.Length <= 1)
            {
                Debug.LogError("No commands detected");
                return originalText;
            }

            result = originalText;
            foreach(var actionDescription in allActions)
            {
                string callingString = actionDescription.ActionCallingString;
                result = result.Replace($"{commandCharacter}{callingString}", 
                    InputStatic.ConrolsNames.RefilterControlText(
                    GetBindingByCall(callingString)));
            }
            
            return result;
        }
        #endregion

        #region Action Calls
        /// <summary>
        /// Defines the action reference in a simplified class
        /// </summary>
        public class InputActionDescription
        {
            private InputAction action;
            public InputAction Action
            {
                get
                {
                    return action;
                }
                set
                {
                    if (action == null)
                        action = value;
                }
            }
            private string actionCallingString;
            public string ActionCallingString
            {
                get
                {
                    return actionCallingString;
                }
                set
                {
                    if (actionCallingString == null)
                        actionCallingString = value;
                }
            }
            
        }

        

        public static InputActionDescription moveAction = new()
        {
            Action = inputNames.Player.Move,
            ActionCallingString = "move"
        };

        public static InputActionDescription lookAction = new()
        {
            Action = inputNames.Player.Look,
            ActionCallingString = "look"
        };

        public static InputActionDescription fireAction = new()
        {
            Action = inputNames.Player.Fire,
            ActionCallingString = "fire"
        };

        public static InputActionDescription jumpAction = new()
        {
            Action = inputNames.Player.Jump,
            ActionCallingString = "jump"
        };

        public static InputActionDescription sprintAction = new()
        {
            Action = inputNames.Player.Sprint,
            ActionCallingString = "sprint"
        };

        public static InputActionDescription grabAction = new()
        {
            Action = inputNames.Player.Grab,
            ActionCallingString = "grab"
        };

        public static InputActionDescription pauseAction = new()
        {
            Action = inputNames.Player.Pause,
            ActionCallingString = "pause"
        };

        public static InputActionDescription boltIndexAction = new()
        {
            Action = inputNames.Player.BoltIndexScroll,
            ActionCallingString = "scroll"
        };

        public static InputActionDescription throwAction = new()
        {
            Action = inputNames.Player.Throw,
            ActionCallingString = "throw"
        };

        public static InputActionDescription boltSelectedOne = new()
        {
            Action = inputNames.Player.SetBolt1,
            ActionCallingString = "b1"
        };

        public static InputActionDescription boltSelectedTwo = new()
        {
            Action = inputNames.Player.SetBolt2,
            ActionCallingString = "b2"
        };

        public static InputActionDescription boltSelectedThree = new()
        {
            Action = inputNames.Player.SetBolt3,
            ActionCallingString = "b3"
        };

        public static InputActionDescription boltSelectedFour = new()
        {
            Action = inputNames.Player.SetBolt4,
            ActionCallingString = "b4"
        };

        public static InputActionDescription boltSelectedFive = new()
        {
            Action = inputNames.Player.SetBolt5,
            ActionCallingString = "b5"
        };

        public static InputActionDescription boltSelectedSix = new()
        {
            Action = inputNames.Player.SetBolt6,
            ActionCallingString = "b6"
        };

        public static InputActionDescription boltSelectedSeven = new()
        {
            Action = inputNames.Player.SetBolt7,
            ActionCallingString = "b7"
        };

        public static InputActionDescription boltSelectedEight = new()
        {
            Action = inputNames.Player.SetBolt8,
            ActionCallingString = "b8"
        };

        public static InputActionDescription boltSelectedNine = new()
        {
            Action = inputNames.Player.SetBolt9,
            ActionCallingString = "b9"
        };

        public static InputActionDescription sprintTapAction = new()
        {
            Action = inputNames.Player.SprintTap,
            ActionCallingString = "tap"
        };

        public static InputActionDescription incrementBoltAction = new()
        {
            Action = inputNames.Player.IncrementBoltChoice,
            ActionCallingString = "increase"
        };

        public static InputActionDescription decrementBoltAction = new()
        {
            Action = inputNames.Player.DecrementBoltChoice,
            ActionCallingString = "decrease"
        };

        public static InputActionDescription[] allActions =
        {
            moveAction,
            lookAction,
            fireAction,
            jumpAction,
            sprintAction,
            grabAction,
            pauseAction,
            boltIndexAction,
            throwAction,
            boltSelectedOne,
            boltSelectedTwo,
            boltSelectedThree,
            boltSelectedFour,
            boltSelectedFive,
            boltSelectedSix,
            boltSelectedSeven,
            boltSelectedEight,
            boltSelectedNine,
            sprintTapAction,
            incrementBoltAction,
            decrementBoltAction
    };
        #endregion

        #region Control Schemes
        public static InputControlScheme KeyboardControlSchemeTag()
        {
            return inputNames.KeyboardMouseScheme;
        }

        public static InputControlScheme GamepadControlScheme()
        {
            return inputNames.GamepadScheme;
        }
        #endregion
    }

    /// <summary>
    /// Returns input related methods withn unity confines
    /// </summary>
    public static class InputData
    {
        /// <summary>
        /// Checks if a gamepad is connected
        /// </summary>
        /// <returns></returns>
        ///     returns true if count of gamepads are above 0
        public static bool IsAGamepadConnected()
        {
            return Gamepad.all.Count > 0;
        }
    }

    public static class ConrolsNames
    {
        public static string RefilterControlText(string originalInput)
        {
            string result = "";

            result = originalInput;

            foreach(var inputPreference in allPreferences)
            {
                if (originalInput.Contains(inputPreference.OriginalInputName))
                {
                    result = inputPreference.PreferedInputName;
                    return result;
                }
            }

            return result;
        }


        public class ControlNamePreference
        {
            private string originalInputName;
            public string OriginalInputName
            {
                get { return originalInputName; }
                set
                {
                    if (originalInputName == null)
                        originalInputName = value;
                }
            }

            private string preferedInputName;
            public string PreferedInputName
            {
                get { return preferedInputName; }
                set
                {
                    if (preferedInputName == null)
                        preferedInputName = value;
                }
            }
        }

        public static ControlNamePreference leftClick = new()
        {
            OriginalInputName = "LMB",
            PreferedInputName = "Left Click"
        };

        public static ControlNamePreference rightClick = new()
        {
            OriginalInputName = "RMB",
            PreferedInputName = "Right Click"
        };

        public static ControlNamePreference leftStick = new()
        {
            OriginalInputName = "LS",
            PreferedInputName = "Left Analog Stick"
        };

        public static ControlNamePreference rightStick = new()
        {
            OriginalInputName = "RS",
            PreferedInputName = "Right Analog Stick"
        };

        public static ControlNamePreference leftTrigger = new()
        {
            OriginalInputName = "LT",
            PreferedInputName = "Left Trigger"
        };

        public static ControlNamePreference rightTrigger = new()
        {
            OriginalInputName = "RT",
            PreferedInputName = "Right Trigger"
        };

        public static ControlNamePreference leftBumper = new()
        {
            OriginalInputName = "LB",
            PreferedInputName = "Left Bumper"
        };

        public static ControlNamePreference rightBumper = new()
        {
            OriginalInputName = "RB",
            PreferedInputName = "Right Bumper"
        };

        public static ControlNamePreference[] allPreferences = 
        {
            leftClick,
            rightClick,
            leftStick,
            rightStick,
            leftTrigger,
            rightTrigger,
            leftBumper,
            rightBumper
        };
    }
}
