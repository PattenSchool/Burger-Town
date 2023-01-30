using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelButton : MonoBehaviour, IObjectEvent
{
    public SaveLoadManager saveLoadManagerTest;
    public void IOnEventTriggered()
    {
        //saveLoadManagerTest.LoadSave();
    }
}
