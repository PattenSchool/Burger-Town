using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationPlayer : MonoBehaviour
{
    #region AnimationStrings
    [Header("Animation Strings")]

    [Tooltip("Shoot animation string")]
    [SerializeField]
    private string shootCallString;

    [Tooltip("Idle animation string")]
    [SerializeField]
    private string idleCallString;

    [Tooltip("Defeated animation string")]
    [SerializeField]
    private string defeatedCallString;
    #endregion

    #region Animator
    [Header("Animator")]
    public Animator animationShell;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        animationShell = GetComponent<Animator>();
    }
    #endregion


    #region public call methods
    public void PlayShootAnimation()
    {
        PlayAnimation(shootCallString);
    }

    public void PlayDefeatedAnimation()
    {
        PlayAnimation(defeatedCallString);
    }

    public void PlayIdleAnimation()
    {
        PlayAnimation(idleCallString);
    }
    #endregion

    #region Call Method
    /// <summary>
    /// Used to play an animtion
    /// </summary>
    /// <param name="animation"></param>
    ///     The name of the animation state being played
    private void PlayAnimation(string animation)
    {
        animationShell.Play(animation);
    }
    #endregion
}
