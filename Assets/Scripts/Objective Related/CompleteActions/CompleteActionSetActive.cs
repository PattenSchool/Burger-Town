using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteActionSetActive : AbstractCompleteAction
{
    public GameObject thisGameObject;

    public bool isActive;

    public override void CompleteAction()
    {
        thisGameObject.SetActive(isActive);
    }
}
