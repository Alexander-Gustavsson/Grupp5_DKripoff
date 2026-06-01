using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControllerUI : MonoBehaviour
{
    public static float menuVol;
    public static float SFXvol;
    public static float musicVol;

    public static Coroutine musicSmoother;

    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        Action onChangeValue;


        switch (gameObject.name)
        {
            case ("SFX Slider"):
                onChangeValue = ChangeSFXVolume;
                slider.value = AudioManager.SFXvolume;
                break;
            case ("Music Slider"):
                onChangeValue = ChangeMusicVolume;
                slider.value = MusicPlayer.instance.volume;
                break;
            default:
                slider.value = MusicPlayer.instance.volume;
                onChangeValue = ChangeMusicVolume;
                break;
        }
        slider.onValueChanged.AddListener(delegate { onChangeValue(); });
    }

    private void ChangeSFXVolume()
    {
        print("Go");
        AudioManager.SFXvolume = slider.value;
    }

    private void ChangeMusicVolume()
    {
        if(musicSmoother != null)
        {
            StopCoroutine(musicSmoother);
        }

        MusicPlayer.instance.SmoothSound(slider.value, 1f);
    }
}
