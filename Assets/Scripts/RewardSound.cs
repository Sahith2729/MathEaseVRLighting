using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSound : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip audioClip; // Assign this in the Inspector

    private void Start()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    // Method to play the sound
    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}
