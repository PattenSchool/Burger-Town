using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerState : StateMachineBehaviour
{
    #region Variables
    [Tooltip("The boss's head")]
    private Transform headTransformData;

    [Tooltip("Player's position data")]
    private Transform playerTransformData;

    [Tooltip("player's position")]
    private Vector3 playerPosition;
    #endregion

    #region Unity Methods
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set up the variables
        SetUpHead(animator);
        SetUpPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Make enemy look at player
        playerPosition = playerTransformData.position;
        headTransformData.LookAt(playerPosition);
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
    #endregion

    #region Set Up Methods
    private void SetUpHead(Animator animator)
    {
        if (headTransformData == null)
        {
            headTransformData = animator.gameObject.GetComponent<BossBehaviorVariables>().Head.transform;
        }
    }

    private void SetUpPlayer()
    {
        if (playerTransformData == null)
        {
            playerTransformData = PlayerStatic.Player.transform;
        }
    }
    #endregion
}
