using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObjective : MonoBehaviour
{
    public string ObjectiveName;
    public string ObjectiveDescription;

    [HideInInspector]
    public bool isComplete;
    public abstract void UpdateThis();

    public void MarkComplete()
    {
        isComplete = true;
    }
}
