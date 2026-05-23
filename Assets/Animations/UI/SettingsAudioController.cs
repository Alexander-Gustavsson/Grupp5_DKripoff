using UnityEngine;
using UnityEngine.UI;

public class SettingsAudioController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;

    [Header("UI")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (musicSource != null && musicSlider != null)
        {
            musicSlider.value = GameSettings.MusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = GameSettings.SfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }
        if (musicSource != null)
        {
            musicSource.volume = GameSettings.MusicVolume;
        }
    }

    public void SetMusicVolume(float value)
    {
        GameSettings.MusicVolume = value;
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }
    public void SetSfxVolume(float value)
    {
        GameSettings.SfxVolume = value;
    }
}