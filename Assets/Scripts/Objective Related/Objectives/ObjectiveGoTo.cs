using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates a create menu to make quest objects
//[CreateAssetMenu(fileName = "New Objective", menuName = "Objective/GoToLocation")]
public class ObjectiveGoTo : AbstractObjective
{
    //public string QuestName { get; set;}
    //public string QuestDescription { get; set;}
    //public bool isComplete { get; set;}

    public GameObject _object;

    public GameObject location;

    //public Vector3 locationVector;

    public float radius;

    public override void UpdateThis()
    {
        if (Vector3.Distance(_object.transform.position, location.transform.position) < radius)
        {
            MarkComplete();
        }
    }
}
