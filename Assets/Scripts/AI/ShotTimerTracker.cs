using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTimerTracker : StateMachineBehaviour
{
    #region Boss Behavior Manager
    [SerializeField]
    private BossBehaviorVariables bossBehaviordata;
    #endregion

    #region Shot Related Variables
    [Tooltip("Shot Trigger Tag")]
    [SerializeField]
    private string shotTriggerTag = "shotTrigger";
    #endregion

    #region Timer Related
    [Tooltip("time between shots in seconds")]
    private float timeBetweenShots = 0f;

    [Tooltip("Shots delay in seconds")]
    [SerializeField, Min(0f)]
    private float delayInSeconds = 0f;

    [Tooltip("Current time of timer")]
    private float shotTimer = 0f;

    /// <summary>
    /// Resets the active time with time between shots
    /// </summary>
    private void SetShotTimer()
    {
        shotTimer = timeBetweenShots + delayInSeconds;
    }

    /// <summary>
    /// Subtracts the ammount of time that has passed
    /// </summary>
    /// <param name="deltaTime"></param>
    ///     The time hange between each update of this method
    private void SubtractTime(float deltaTime)
    {
        shotTimer -= deltaTime;
    }
    #endregion

    #region Unity Methods
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get boss behavior data
        if (bossBehaviordata == null)
        {
            bossBehaviordata = animator.GetComponent<BossBehaviorVariables>();
            timeBetweenShots = bossBehaviordata.TimeBetweenShots;
        }

        //Reset trigger
        animator.ResetTrigger(shotTriggerTag);

        //Set timer
        SetShotTimer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Subtract timer
        SubtractTime(Time.deltaTime);

        //If timer < 0, then trigger shot
        if (shotTimer <= 0f)
        {
            animator.SetTrigger(shotTriggerTag);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

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

    #region Timer Coroutines
    private IEnumerator ToShotState(Animator animator)
    {
        //Wait for seconds
        yield return new WaitForSeconds(timeBetweenShots);

        //Activate trigger
        animator.SetTrigger(shotTriggerTag);
    }
    #endregion
}
