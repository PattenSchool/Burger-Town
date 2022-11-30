using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteActionSpawnItem : AbstractCompleteAction
{
    public GameObject item;

    public GameObject locationObject;

    private Vector3 location;

    public void Start()
    {
        location = locationObject.transform.position;
    }

    public override void CompleteAction()
    {
        if (!item.activeInHierarchy)
        {
            item.SetActive(true);
        }

        item.transform.position = location;
    }
}
