using UnityEngine;
using UnityEngine.Events;

public class AnswerValidator : MonoBehaviour {
    [Header("Answer Settings")]
    
    
    public AD_L1_VoiceManager voiceManager;
    private bool hasResumed = false;
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    [Header("Events")]
    public UnityEvent OnCorrectAnswer;
    public UnityEvent OnWrongAnswer;

    private void Start() {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void ValidateCorrectAnswer() {
        Debug.Log("Correct Answer!");
        if (correctSound != null && audioSource != null) {
            if (!hasResumed)
            {
                voiceManager.ResumePlayback();
                hasResumed = true; // Prevent multiple triggers
            }
            audioSource.PlayOneShot(correctSound);
        }
        OnCorrectAnswer?.Invoke();
    }

    public void ValidateWrongAnswer() {
        Debug.Log("Wrong Answer!");
        if (wrongSound != null && audioSource != null) {
            audioSource.PlayOneShot(wrongSound);
        }
        OnWrongAnswer?.Invoke();
    }
}