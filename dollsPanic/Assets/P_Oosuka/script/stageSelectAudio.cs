using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageSelectAudio : MonoBehaviour {

    [SerializeField]
    AudioSource BGM;
    [SerializeField]
    AudioSource cursorSound;
    [SerializeField]
    AudioSource enterSound;

    public void BGMvolume(float volume)
    {
        BGM.volume = volume;
    }

    public void PlayCursor()
    {
        cursorSound.PlayOneShot(cursorSound.clip);
    }
    
    public void PlayEnter()
    {
        enterSound.PlayOneShot(enterSound.clip);
    }
}
