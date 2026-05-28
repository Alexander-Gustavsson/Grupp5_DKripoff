using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    [SerializeField] private float volume;

    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip prepClip;
    [SerializeField] private AudioClip combatClip;

    [SerializeField] private AudioSource playerCurrent;
    [SerializeField] private AudioSource playerNext;


    [SerializeField] private float transitionDuration;



    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        playerCurrent.Play();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SmoothSound(float target, float time)
    {
        StartCoroutine(SmoothSoundRoutine(target, time));
    }

    private IEnumerator SmoothSoundRoutine(float target, float time)
    {
        float currentTime = 0f;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        float startVolume = audioSource.volume;

        float volumeDiff = target - startVolume;

        while (currentTime <= time)
        {
            currentTime += Time.deltaTime;

            audioSource.volume = startVolume + volumeDiff * (currentTime/time);

            yield return null;
        }

        audioSource.volume = target;
    }

    public void PlayCombat()
    {
        playClip(combatClip);
    }
    public void PlayMenu()
    {
        playClip(menuClip);
    }
    public void PlayPreparation()
    {
        playClip(prepClip);
    }

    public void playClip(AudioClip clip)
    {
        playerNext.clip = clip;
        playerNext.Play();

        StartCoroutine(SmoothTransition());
    }

    private IEnumerator SmoothTransition()
    {
        float timePassed = 0f;

        // increase volume
        while (timePassed < transitionDuration)
        {
            timePassed += Time.deltaTime;

            this.playerCurrent.volume -= (Time.deltaTime / transitionDuration) * volume;
            this.playerNext.volume += (Time.deltaTime / transitionDuration) * volume;

            yield return null;
        }

        this.playerCurrent.volume = 0;
        this.playerCurrent.Stop();
        this.playerNext.volume = volume;

        this.playerCurrent = this.playerNext;

        // swap references
    }
}
