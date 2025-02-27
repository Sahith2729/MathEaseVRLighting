using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAudio : MonoBehaviour
{public AudioSource audioSource; // Assign the AudioSource in the Inspector
    public AudioClip audioClip;     // Assign the AudioClip in the Inspector

    void Start()
    {
        // Ensure the AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector.");
            return;
        }

        // Assign the audio clip to the audio source
        audioSource.clip = audioClip;

        // Start the coroutine to play the audio after 3 seconds
        StartCoroutine(PlayAudioWithDelay(3f));
    }

    private System.Collections.IEnumerator PlayAudioWithDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the audio clip
        audioSource.Play();
    }
}
