using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a script to make adding a track easier
/// </summary>
public class AddMusic : MonoBehaviour
{
    #region Audio
    [Header("Music")]

    [Tooltip("The clip being played")]
    [SerializeField]
    private AudioClip audioClip;
    #endregion

    private void Start()
    {
        //Get the first instance of that
        var audioManager = AudioManager.instance;
        AudioManager.instance.AddTrack(audioClip);
    }
}
