using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates a create menu to make quest objects
//[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/New Quest")]
[Serializable]
public class Quest// : ScriptableObject
{
    public bool isActive = false;

    public string QuestName;

    [SerializeField]
    public List<int> NextQuests;

    [HideInInspector]
    public void SetActive(bool active)
    {
        isActive = active;
    }

    public List<AbstractObjective> objectives;
    public List<AbstractCompleteAction> completeActions;
}
