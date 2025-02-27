using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_L1_VEH : MonoBehaviour
{
    public AD_L1_VoiceManager voiceManager;

    void Start()
    {
        if (voiceManager.voiceClips.Count > 0)
        {
            // Add events for each voice clip dynamically
            //voiceManager.voiceClips[0].events.Add(new VoiceEvent { delayTime = 2f, eventAction = EventOne });
            //voiceManager.voiceClips[0].events.Add(new VoiceEvent { delayTime = 5f, eventAction = EventTwo });

            //voiceManager.voiceClips[1].events.Add(new VoiceEvent { delayTime = 3f, eventAction = AnotherEvent });
        }
    }

    private void EventOne()
    {
        Debug.Log("Event One triggered after 2 seconds!");
    }

    private void EventTwo()
    {
        Debug.Log("Event Two triggered after 5 seconds!");
    }

    private void AnotherEvent()
    {
        Debug.Log("Another event triggered for the second clip!");
    }
}