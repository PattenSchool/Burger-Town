using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : StateMachineBehaviour
{
    #region Boss Behavior Data
    [Tooltip("Boss Behaviors data")]
    private BossBehaviorVariables bossBehaviorVariables;
    #endregion


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossBehaviorVariables == null)
            bossBehaviorVariables = animator.GetComponent<BossBehaviorVariables>();

        //Get the transform's direction
        Vector3 directionFacing = bossBehaviorVariables.LookingDirection;

        //Fire at the direction being faced
        var ammoTemplate = bossBehaviorVariables.Ammo;
        var spawnedAmmo =
            ObjectPooling.Spawn(ammoTemplate.gameObject, 
            directionFacing + bossBehaviorVariables.Head.transform.position, bossBehaviorVariables.Head.transform.rotation);

        spawnedAmmo.GetComponent<BoltTemplate>().OnFire(animator.gameObject, directionFacing);


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
