using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Detta script läggs till som en StateMachineBehaviour på en animation i animatorn. När animationen slutar spelas det ljud som är valt i sound-variabeln upp. Volymen kan justeras i volume-variabeln.

public class PlaySoundExit : StateMachineBehaviour
{

    [SerializeField] private SoundType sound;
    [SerializeField, Range(0, 1)] private float volume = 1;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.PlaySound(sound, volume);
    }
}
