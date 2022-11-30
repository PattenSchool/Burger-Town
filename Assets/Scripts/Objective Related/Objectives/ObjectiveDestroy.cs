using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDestroy : AbstractObjective
{
    public GameObject objectToDestroy;

    public override void UpdateThis()
    {
        if (objectToDestroy == null || !objectToDestroy.activeInHierarchy)
        {
            MarkComplete();
        }
    }
}
