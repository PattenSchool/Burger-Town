using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountDown : StateMachineBehaviour
{
    #region Component related
    [Header("Component Related")]

    [Tooltip("The varaibles being held to easily access boss information")]
    [SerializeField]
    private BossVariables bossVariables;
    #endregion

    #region animator related
    [Header("Timer related")]

    [SerializeField, Min(0f)]
    private float timer = 0f;

    [SerializeField]
    private string triggerName = "";
    #endregion

    #region Unity Methods
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //If reference isn't existing, add in the boss varaibles
        if (bossVariables == null)
            bossVariables = animator.GetComponent<BossVariables>();

        //Set up the timer
        timer = bossVariables.Cooldown;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !PlayerStatic.HasConversation())
            animator.SetTrigger(triggerName);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    #endregion
}
