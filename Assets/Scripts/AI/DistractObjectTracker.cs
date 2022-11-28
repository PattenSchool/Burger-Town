using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractObjectTracker : StateMachineBehaviour
{
    #region Boss Variables
    [Tooltip("Boss Behaviors Data")]
    [SerializeField]
    private BossBehaviorVariables bossBehaviorData;

    [Tooltip("The boss's head")]
    [SerializeField]
    private GameObject bossHead;
    #endregion

    #region Tags
    [Tooltip("is distracted tag")]
    [SerializeField]
    private string isDistractedTag = "isDistracted";
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get boss behavior variable data once
        if (bossBehaviorData == null)
            bossBehaviorData = animator.gameObject.GetComponent<BossBehaviorVariables>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ///Set distracted if distracted objct exists in eye sight

        //check if distractable object exists, if not, then skip
        if (!bossBehaviorData.DistractableObject.gameObject.activeInHierarchy)
        {
            animator.SetBool(isDistractedTag, false);
            return;
        }
        else
        {
            animator.SetBool(isDistractedTag, true);
        }

        //TODO: Check if distractable object is in eye sight
        #region Define Ray
        //Ray eyeSight = new Ray()
        //{
        //    origin = bossBehaviorData.Head.transform.position,
        //    direction = bossBehaviorData.LookingDirection,
        //};
        #endregion

        //Physics.Raycast(eyeSight);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
