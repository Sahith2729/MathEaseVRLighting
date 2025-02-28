using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.Play();
        }
    }
}
