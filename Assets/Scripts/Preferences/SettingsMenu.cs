using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;

    public TextMeshProUGUI musicText;

    public TextMeshProUGUI sfxText;

    public UnityEngine.UI.Slider musicSlider;

    public UnityEngine.UI.Slider sfxSlider;

    public float MusicVolume = 0.05f;

    public string MusicName = "Music";

    public float SFXVolume = 0.05f;

    public string SFXName = "SFX";

    private void Awake()
    {
        if (SettingsMenu.instance == null)
        {
            SettingsMenu.instance = this;
        }
        else if (SettingsMenu.instance != this)
        {
            Destroy(this);
        }

        if (PlayerPrefs.HasKey(MusicName))
        {
            MusicVolume = PlayerPrefs.GetFloat(MusicName);
            //AudioListener.volume = MusicVolume;

            musicSlider.value = MusicVolume * 100f;
        }

        if (PlayerPrefs.HasKey(SFXName))
        {
            SFXVolume = PlayerPrefs.GetFloat(SFXName);
            //AudioListener.volume = SFXVolume;

            sfxSlider.value = SFXVolume * 100f;
        }
    }

    /*
    [RuntimeInitializeOnLoadMethod]
    static void SetMusic()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            //AudioListener.volume = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            //AudioListener.volume = 0.01f;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void SetSFX()
    {
        if (PlayerPrefs.HasKey("SFX"))
        {
            //AudioListener.volume = PlayerPrefs.GetFloat("SFX");
        }
        else
        {
            //AudioListener.volume = 0.01f;
        }
    }
    */


    public void SetMusicVolume(float value)
    {
        musicText.text = value.ToString("N0");
        MusicVolume = value / 100;

        //AudioListener.volume = MusicVolume;

        PlayerPrefs.SetFloat(MusicName, MusicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxText.text = value.ToString("N0");
        SFXVolume = value / 100;

        //AudioListener.volume = MusicVolume;

        PlayerPrefs.SetFloat(SFXName, SFXVolume);
    }

}
