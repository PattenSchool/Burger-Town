using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDefeatState : StateMachineBehaviour
{
    #region Boss Variable Data
    [Tooltip("The boss's variable data")]
    private BossBehaviorVariables bossVariablesData;
    #endregion

    #region Is Defeated Trigger tag
    [Tooltip("The is defeated tag")]
    private string isDeteatedTag = "isDefeated";
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossVariablesData == null)
        {
            bossVariablesData = animator.gameObject.GetComponent<BossBehaviorVariables>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check if defeated, and if so, then defeat
        if (bossVariablesData.IsDefeated)
        {
            animator.SetTrigger(isDeteatedTag);
        }
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
