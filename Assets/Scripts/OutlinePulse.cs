using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinePulse : MonoBehaviour
{
    public List<Outline> targetOutlines; // List to hold multiple Outline components
    public float minWidth = 0f;
    public float maxWidth = 10f;
    public float duration = 1f;
    public float totalDuration = 3.3f; // Total duration for pulsing
    public float delayBeforeStart = 0f; // Delay before pulsing starts

    void Start()
    {
        if (targetOutlines == null || targetOutlines.Count == 0)
        {
            Debug.LogError("OutlinePulse: No Outline components assigned!");
            return;
        }
        StartCoroutine(PulseOutline());
    }

    IEnumerator PulseOutline()
    {
        yield return new WaitForSeconds(delayBeforeStart); // Wait 4 seconds before starting

        float time = 0f;
        bool increasing = true;
        float elapsedTime = 0f; // Track total elapsed time
        
        while (elapsedTime < totalDuration)
        {
            while (time < duration)
            {
                float t = time / duration;
                t = t * t * (3f - 2f * t); // Smoothstep (sine-in sine-out approximation)
                
                foreach (var outline in targetOutlines)
                {
                    if (outline != null)
                    {
                        outline.OutlineWidth = Mathf.Lerp(minWidth, maxWidth, increasing ? t : 1 - t);
                    }
                }
                
                time += Time.deltaTime;
                elapsedTime += Time.deltaTime; // Increase total elapsed time
                
                if (elapsedTime >= totalDuration) 
                    yield break; // Stop if total duration is reached
                
                yield return null;
            }
            
            increasing = !increasing;
            time = 0f;
            yield return new WaitForSeconds(0.5f); // Pause before reversing
            
            elapsedTime += 0.5f; // Account for wait time
        }
    }
}
