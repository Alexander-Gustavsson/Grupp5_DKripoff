using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static float menuVol;
    public static float SFXvol;
    public static float musicVol;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Slider slider = GetComponent<Slider>();
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

    }

    private void ChangeSFXVolume()
    {

    }

    private void ChangeMusicVolume()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
