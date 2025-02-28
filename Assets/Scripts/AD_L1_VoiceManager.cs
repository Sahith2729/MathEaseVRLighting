using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VoiceEvent
{
    public float delayTime;
    public UnityEvent eventAction;
}

[System.Serializable]
public class VoiceClipData
{
    public AudioClip audioClip;
    public float preClipDelay;
    public bool pauseAfterClip = false; // Pause after this clip (set in Inspector)
    public List<VoiceEvent> events = new List<VoiceEvent>();
}

public class AD_L1_VoiceManager : MonoBehaviour
{
    public AudioSource orbAudioSource;
    public List<VoiceClipData> voiceClips;

    private int currentClipIndex = 0;
    private Coroutine voiceCoroutine;
    private bool isPaused = false;

    void Start()
    {
        if (voiceClips.Count > 0)
        {
            PlayNextVoice();
        }
    }

    private void PlayNextVoice()
    {
        if (currentClipIndex < voiceClips.Count)
        {
            VoiceClipData clipData = voiceClips[currentClipIndex];

            voiceCoroutine = StartCoroutine(HandleVoicePlayback(clipData));
        }
    }

    private IEnumerator HandleVoicePlayback(VoiceClipData clipData)
    {
        yield return new WaitForSeconds(clipData.preClipDelay);

        orbAudioSource.clip = clipData.audioClip;
        orbAudioSource.Play();

        foreach (var voiceEvent in clipData.events)
        {
            StartCoroutine(TriggerEventWithDelay(voiceEvent));
        }

        yield return new WaitForSeconds(orbAudioSource.clip.length);

        if (clipData.pauseAfterClip)
        {
            isPaused = true; // Set pause state after playing the clip
        }
        else
        {
            currentClipIndex++; // Increment only if not pausing
            PlayNextVoice();
        }
    }

    private IEnumerator TriggerEventWithDelay(VoiceEvent voiceEvent)
    {
        yield return new WaitForSeconds(voiceEvent.delayTime);
        voiceEvent.eventAction?.Invoke();
    }

    public void ResumePlayback()
    {
        if (isPaused)
        {
            isPaused = false;
            currentClipIndex++; // Move to the next clip before resuming
            PlayNextVoice();
        }
    }
}
