using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        //Time Scale thing
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
