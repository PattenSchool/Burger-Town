using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCount : MonoBehaviour
{
    public Text numTargets;

    // Update is called once per frame
    void Update()
    {
        //Displays the number of targets remaining.
        numTargets.text = "Targets Remaining: " + GateTrigger.targetNum.ToString();

        //Tells the player that the gate is open when there are no targets left.
        if(GateTrigger.targetNum <= 0)
        {
        numTargets.text = "Gate is now open.";
        }
    }

}
