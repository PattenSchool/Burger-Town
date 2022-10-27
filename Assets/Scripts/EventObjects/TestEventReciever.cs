using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventReciever : MonoBehaviour, IObjectEvent
{
    public void IOnEventTriggered()
    {
        this.gameObject.SetActive(false);
    }
}
