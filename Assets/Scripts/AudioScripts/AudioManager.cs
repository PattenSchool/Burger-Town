using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private int SFXChannelAmount;

    private AudioSource[] SFXSources;

    private int curSFX = 0;

    [SerializeField]
    private AudioClip testClip;

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
        SFXSources = new AudioSource[SFXChannelAmount];

        for (int i = 0; i < SFXSources.Length; i++)
        {
            SFXSources[i] = gameObject.AddComponent<AudioSource>();

            SFXSources[i].spatialBlend = 0;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSources[curSFX].clip = clip;

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
        thing.Play();
        StartCoroutine(DestroyAfterTime(GO, clip.length));
    }

    private void OnEnable()
    {
        PlaySFX(testClip, Vector3.zero);
    }

    private IEnumerator DestroyAfterTime(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(go);
    }
}
