using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNoise : MonoBehaviour
{
    #region Audio
    [Header("Audio")]

    [Tooltip("The audioclip to be played")]
    [SerializeField]
    private AudioClip soundSFX;
    #endregion

    private void PlayNoise()
    {
        AudioManager.instance.PlaySFX(soundSFX);
    }
}
