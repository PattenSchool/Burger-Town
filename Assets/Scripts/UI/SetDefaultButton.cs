using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class SetDefaultButton : MonoBehaviour 
{ 
    public Button firstSelected; 
    private void OnEnable() 
    { 
        if(firstSelected == null) 
        { 
            return; 
        } 
        firstSelected.Select(); 
    } 
}
