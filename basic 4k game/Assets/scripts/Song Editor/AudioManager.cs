using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Button playButton;
    public Button pauseButton;
    public Button stopButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayAudio);
        pauseButton.onClick.AddListener(PauseAudio);
        stopButton.onClick.AddListener(StopAudio);

        UpdateButtonInteractivity();
    }

    void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            UpdateButtonInteractivity();
        }
    }

    void PauseAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            UpdateButtonInteractivity();
        }
    }

    void StopAudio()
    {
        if (audioSource.isPlaying || audioSource.time > 0)
        {
            audioSource.Stop();
            audioSource.time = 0f;  // Reset the audio to the beginning
            UpdateButtonInteractivity();
        }
    }

    void UpdateButtonInteractivity()
    {
        // cannot press play when song is playing. cannot press pause when song isn't
        playButton.interactable = !audioSource.isPlaying;
        pauseButton.interactable = audioSource.isPlaying;
        stopButton.interactable = audioSource.isPlaying || audioSource.time > 0;
    }
}