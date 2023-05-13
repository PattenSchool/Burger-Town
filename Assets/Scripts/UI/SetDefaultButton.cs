using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class SetDefaultButton : MonoBehaviour 
{
    #region UI Elements
    public Button firstSelected;
    #endregion

    #region Tracker Variables
    private bool isControllerConnectedLastUpdate = false;
    #endregion

    #region Unity Methods
    private void OnEnable() 
    { 
        if(firstSelected == null) 
        { 
            return; 
        } 

        if (InputStatic.InputData.IsAGamepadConnected())
            firstSelected.Select(); 
    }

    private void Update()
    {
        //Temp Get current isControlller updated
        bool isControllerConnectedThisUpdate = InputStatic.InputData.IsAGamepadConnected();

        //Compare
        if (isControllerConnectedThisUpdate != isControllerConnectedLastUpdate)
            firstSelected.Select();

        //Set new
        isControllerConnectedLastUpdate = isControllerConnectedThisUpdate;
    }
    #endregion
}
