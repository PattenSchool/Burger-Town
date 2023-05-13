using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class SetDefaultButton : MonoBehaviour 
{
    #region UI elements
    public Button firstSelected;
    #endregion

    #region Update checks
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
        //Get this update's check
        bool isControllerConnectedThisUpdate = InputStatic.InputData.IsAGamepadConnected();

        //Check ifcontroller is connected
        if (isControllerConnectedThisUpdate != isControllerConnectedLastUpdate)
            firstSelected.Select();


        //Update last update check
        isControllerConnectedLastUpdate = isControllerConnectedThisUpdate;
    }
    #endregion
}
