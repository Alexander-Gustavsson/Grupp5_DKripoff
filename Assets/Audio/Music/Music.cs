using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.GetComponent<AudioSource>().Play();
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
}
