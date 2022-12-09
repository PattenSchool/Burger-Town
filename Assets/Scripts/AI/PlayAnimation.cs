using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : StateMachineBehaviour
{
    #region Behaviors
    private BossAnimationPlayer animationPlayer;
    private BossBehaviorVariables behaviorVariables;
    #endregion

    #region Add animation type
    private enum AnimationType
    {
        shoot,
        idle,
        defeated
    };

    [SerializeField]
    private AnimationType animationType;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (behaviorVariables == null)
            behaviorVariables = animator.gameObject.GetComponent<BossBehaviorVariables>();
        if (animationPlayer == null)
            animationPlayer = behaviorVariables.AnimationShell;

        //Play animation
        if (animationType == AnimationType.shoot)
            animationPlayer.PlayShootAnimation();
        else if (animationType == AnimationType.idle)
            animationPlayer.PlayIdleAnimation();
        else if (animationType == AnimationType.defeated)
            animationPlayer.PlayDefeatedAnimation();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
