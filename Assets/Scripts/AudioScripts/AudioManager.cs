using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private int SFXChannelAmount;

    private AudioSource[] SFXSources;

    private int curSFX = 0;

    private AudioSource bgm;
    private List<AudioClip> tracks = new List<AudioClip> ();
    private int bgmIndex = 0;
    private float currentTime;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
        else if (AudioManager.instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {

        bgm = gameObject.AddComponent<AudioSource>();
        SFXSources = new AudioSource[SFXChannelAmount];

        // Allows for the volume to update when game is paused
        // This was why the volume was not updating in the settings menu
        bgm.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;


        if (tracks.Count > 0)
        {
            bgm.clip = tracks[0];
            bgm.volume = SettingsMenu.instance.MusicVolume;
            bgm.Play();
        }

        for (int i = 0; i < SFXSources.Length; i++)
        {
            SFXSources[i] = gameObject.AddComponent<AudioSource>();

            SFXSources[i].spatialBlend = 0;
        }
    }

    private void Update()
    {
        if (tracks.Count > 0)
        {
            PlayBGMList();
        }
    }



    public void PlaySFX(AudioClip clip)
    {
        SFXSources[curSFX].clip = clip;
        SFXSources[curSFX].volume = SettingsMenu.instance.SFXVolume;
        SFXSources[curSFX].Play();

        curSFX++;

        if (curSFX >= SFXChannelAmount)
        {
            curSFX = 0;
        }
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        GameObject GO = new GameObject();
        AudioSource thing = GO.AddComponent<AudioSource>();
        GO.transform.position = position;
        thing.clip = clip;
        thing.spatialBlend = 1;
        thing.volume = SettingsMenu.instance.SFXVolume;
        thing.Play();
        StartCoroutine(DestroyAfterTime(GO, clip.length));
    }

    private IEnumerator DestroyAfterTime(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(go);
    }

    public void AddTrack(AudioClip clip)
    {
        tracks.Add(clip);
    }

    private void PlayBGMList()
    {
        if (!bgm.isPlaying)
        {

            if (tracks.Count > 0)
            {
                if (bgmIndex < tracks.Count - 1)
                {
                    bgmIndex++;
                    bgm.clip = tracks[bgmIndex];
                    bgm.volume = SettingsMenu.instance.MusicVolume;
                    bgm.Play();
                }
                else if (bgmIndex >= tracks.Count - 1)
                {
                    bgmIndex = 0;
                    bgm.clip = tracks[bgmIndex];
                    bgm.volume = SettingsMenu.instance.MusicVolume;
                    bgm.Play();
                }
            }
            else
            {
                bgm.clip = tracks[0];
                bgm.volume = SettingsMenu.instance.MusicVolume;
                bgm.Play();
            }
        }
    }

    public void UpdateBGM(float volume)
    {
        if (bgm)
        {
            bgm.volume = volume;
        }
    }
}
