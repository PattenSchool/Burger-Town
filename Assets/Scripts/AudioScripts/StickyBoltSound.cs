using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBoltSound : MonoBehaviour
{
    #region Variables
    [Header("Sound")]

    [Tooltip("Sticky Platform SFX")]
    [SerializeField]
    public AudioClip stickySFX;

    [Header("Timer related")]

    [Tooltip("The time in seconds for the sound to play")]
    [SerializeField]
    private float soundDelay = 0.1f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        //if (AudioManager.instance != null)
        //    AudioManager.instance.PlaySFX(stickySFX);

        StartCoroutine(PlaySound());
    }
    #endregion

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(soundDelay);
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(stickySFX);
    }
}
