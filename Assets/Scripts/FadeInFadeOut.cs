using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public float fadeInTime = 1f;  // Time taken to fade in
    public float fadeOutTime = 1f; // Time taken to fade out
    public float visibleDuration = 3f; // Time the object stays fully visible

    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
            SetAlpha(0); // Set initial opacity to 0
            StartCoroutine(FadeRoutine());
        }
    }

    IEnumerator FadeRoutine()
    {
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(visibleDuration);
        yield return StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, originalColor.a, timer / fadeInTime);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(originalColor.a);
    }

    IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0, timer / fadeOutTime);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0);
    }

    void SetAlpha(float alpha)
    {
        if (objectRenderer != null)
        {
            Color newColor = objectRenderer.material.color;
            newColor.a = alpha;
            objectRenderer.material.color = newColor;
        }
    }
}