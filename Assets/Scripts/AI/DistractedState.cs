using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractedState : StateMachineBehaviour
{
    #region Boss Variables
    [Tooltip("Boss's Variables data")]
    [SerializeField]
    private BossBehaviorVariables bossBehaviorVariables;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossBehaviorVariables == null)
            bossBehaviorVariables = animator.gameObject.GetComponent<BossBehaviorVariables>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Update head's looking position
        var distractableObjectPos = bossBehaviorVariables.DistractableObjects[0].transform.position;
        bossBehaviorVariables.Head.transform.LookAt(distractableObjectPos);
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
