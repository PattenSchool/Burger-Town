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
    public TextMeshProUGUI audioValue;

    public UnityEngine.UI.Slider slider;

    private float Volume = 0.05f;

    public string VolumeName = "Volume";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(VolumeName))
        {
            Volume = PlayerPrefs.GetFloat(VolumeName);
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");

            slider.value = Volume * 100f;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void SetVolume()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            AudioListener.volume = 0.01f;
        }
    }


    public void SetAudioValue(float value)
    {
        audioValue.text = value.ToString("N0");
        Volume = value / 100;

        AudioListener.volume = Volume;

        PlayerPrefs.SetFloat(VolumeName, Volume);
    }
    
}
