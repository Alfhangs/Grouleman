using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;

    public void ChangeMusic(AudioClip music)
    {
        BackgroundMusic.Stop();
        BackgroundMusic.clip = music;
        BackgroundMusic.Play();
    }
}
