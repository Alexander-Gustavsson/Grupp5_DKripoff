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
        Action funct;

        switch (gameObject.name)
        {
            case ("SFX Volume"):
                funct = ChangeSFXVolume;
                break;
            case ("Menu Volume"):
                funct = ChangeMenuVolume;
                break;
            default:
                funct = ChangeMusicVolume;
                break;
        }
        slider.onValueChanged.AddListener(delegate { funct(); });
    }

    private void ChangeMenuVolume()
    {
        print("menu");
    }

    private void ChangeSFXVolume()
    {
        print("SFX");

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
