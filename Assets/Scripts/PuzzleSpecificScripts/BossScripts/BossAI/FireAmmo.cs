using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FireAmmo : StateMachineBehaviour
{
    #region Component related
    [Header("Component Related")]

    [Tooltip("The varaibles being held to easily access boss information")]
    [SerializeField]
    private BossVariables bossVariables;
    #endregion

    #region Bolt Related
    [Header("Bolt related")]

    [SerializeField, Min(1f)]
    private float boltSpawnSpace = 1f;

    [Tooltip("For debug purposes")]
    [SerializeField]
    private BoltTemplate bolt;
    #endregion

    #region Unity Methods
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossVariables == null)
        {
            bossVariables = animator.gameObject.GetComponent<BossVariables>();
        }

        var boltChosen = ChooseBolt(bossVariables.PossibleBolts);
        bolt = boltChosen;
        FireBolt(boltChosen, bossVariables.Head.gameObject);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        


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

    #region Bolt methods
    public BoltTemplate ChooseBolt(BoltTemplate[] boltTypes)
    {
        //choose the index
        int chosenIndex = Random.Range(0, boltTypes.Length);

        //Return the gameobject to be fired
        return boltTypes[chosenIndex];
    }

    public void FireBolt(BoltTemplate boltBeingFired, GameObject headGameObject)
    {
        Vector3 directionFacing = headGameObject.transform.forward;

        //Fire at the direction being faced
        var ammoTemplate = boltBeingFired;
        var spawnedAmmo =
            ObjectPooling.Spawn(ammoTemplate.gameObject,
            (directionFacing * boltSpawnSpace) + headGameObject.transform.position,
            headGameObject.transform.rotation);

        spawnedAmmo.GetComponent<BoltTemplate>().OnFire(headGameObject, directionFacing);
    }
    #endregion
}
