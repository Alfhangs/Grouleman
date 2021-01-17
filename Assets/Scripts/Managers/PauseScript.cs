using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject audioManager;
    private AudioSource audio;

    private AudioSource pauseAudio;
    public AudioClip pause;

    private void Start()
    {
        audio = audioManager.GetComponent<AudioSource>();
        pauseAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }

        }
    }
    void Resume()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        audio.Play();
        pauseAudio.PlayOneShot(pause);
    }
    void Paused()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        audio.Pause();
        pauseAudio.PlayOneShot(pause);
    }
}
