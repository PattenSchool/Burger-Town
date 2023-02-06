using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRiseTrigger : MonoBehaviour
{
    public bool isTriggered = false;
    public Animator bridgeRiseAnimation;
    public static int targetNum = 0;
    [SerializeField]
    GameObject[] targets;
    // Start is called before the first frame update
    void Start()
    {
        // Method that sets how many targets are in the area at the start.

        setTargetNum();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Outputs the current number of targets from targetNum into the console window.
        //Debug.Log("Targets Avaliable: " + targetNum);
        // Method that checks if the gate can be lowered
        bridgeMoveCheck();
    }

    void setTargetNum()
    {
        // Populates the targets Gameobject array with gameobjects that have the tag of "Target".
        //if (targets == null)
        //{
            targets = GameObject.FindGameObjectsWithTag("Target");
        //}

        //// Updates the targetNum counter with the number of gameobjects in the targets Gameobject array.
        //foreach (GameObject target in targets)
        //{
            targetNum++;
        //}

        targetNum = targets.Length;
    }

    void bridgeMoveCheck()
    {
        Vector3 moveDirection = Vector3.zero;
        //Checks to see if the targetNum counter reaches 0 or below
        if (targetNum <= 0)
        {
            // Outputs a message to the console if the bridge conditons have been satisfied.
            //Debug.Log("Gate is now open");
            //Plays the idle animation if the Idle Test Object is touched.
            bridgeRiseAnimation.Play("Bridge_Rise");
            Debug.Log("Bridge has risen");
        }
    }
}
