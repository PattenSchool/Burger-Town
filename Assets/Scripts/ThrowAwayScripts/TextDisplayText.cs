using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayText : MonoBehaviour, IObjectEvent
{
    public void Start()
    {
        PlayerStatic.AddToTextQueue("Thing");
    }

    public void IOnEventTriggered()
    {
        print("Thing");
    }
}
