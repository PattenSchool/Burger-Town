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

    public TextMeshProUGUI sensText;

    public UnityEngine.UI.Slider musicSlider;

    public UnityEngine.UI.Slider sfxSlider;

    public UnityEngine.UI.Slider sensSlider;

    public float MusicVolume = 0.05f;

    public string MusicName = "Music";

    public float SFXVolume = 0.05f;

    public float sensitivity;

    public string SFXName = "SFX";

    public string SensitivityName = "Sensitivity";

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

        if (PlayerPrefs.HasKey(SensitivityName))
        {
            sensitivity = PlayerPrefs.GetFloat(SensitivityName);

            sensSlider.value = sensitivity * 100f;

            PlayerStatic.Player.GetComponent<rbCharacterController>().sensitivity = sensitivity;
        }
    }


    public void SetMusicVolume(float value)
    {
        musicText.text = value.ToString("N0");
        MusicVolume = value / 100;

        PlayerPrefs.SetFloat(MusicName, MusicVolume);

        AudioManager.instance.UpdateBGM(MusicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxText.text = value.ToString("N0");
        SFXVolume = value / 100;

        PlayerPrefs.SetFloat(SFXName, SFXVolume);
    }

    public void SetSensitivity(float value)
    {
        sensText.text = value.ToString("N0");

        sensitivity = value / 100;

        PlayerPrefs.SetFloat(SensitivityName, sensitivity);

        if (PlayerStatic.Player != null)
            PlayerStatic.Player.GetComponent<rbCharacterController>().sensitivity = sensitivity;
    }

}
