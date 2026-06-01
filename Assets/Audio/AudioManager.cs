using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Lägg till detta script som en komponent på de GameObjects som ska spela ljud. 

public enum SoundType
{
    // Lägg till ljud här:
    AMBIANCE,
    EXPLOSION,
    UI_PAPER
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    public static float SFXvolume = 1f;
    private static AudioManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        print(SFXvolume + " SFX");
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume * SFXvolume);
        /*Tillåter att spela upp ett slumpat ljud från en lista av olika ljud. 
        Exemeplvis flera olika explosionsljud. Om listan bara har ett ljud funkar det likadandt som koden nedan.*/


        //Spelar upp ett ljud
        //instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume); 
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
