using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSFX : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> tracks = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var track in tracks)
        {
            AudioManager.instance.AddTrack(track);
        }
    }
}