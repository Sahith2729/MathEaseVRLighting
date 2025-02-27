using System.Collections;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public GameObject targetObject; // Assign the GameObject in the Inspector
    public float dissolveDuration = 1.5f; // Time to fully dissolve
    public float delay = 2.3f; // Delay before starting dissolve
    private Coroutine dissolveRoutine;
    private Material dissolveMaterial;
    public AudioSource dissolveSound; // Assign an AudioSource in the Inspector
    public AudioClip dissolveClip; // Assign the sound clip in the Inspector

    private void OnEnable()
    {
        if (targetObject != null)
        {
            Renderer renderer = targetObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                dissolveMaterial = renderer.material;
            }
        }

        if (dissolveMaterial != null && dissolveRoutine != null)
        {
            StopCoroutine(dissolveRoutine);
        }
        dissolveRoutine = StartCoroutine(DissolveEffect());
    }

    private IEnumerator DissolveEffect()
    {
        yield return new WaitForSeconds(delay);

        if (dissolveSound != null && dissolveClip != null)
        {
            dissolveSound.clip = dissolveClip;
            dissolveSound.Play();
        }

        float elapsedTime = 0f;
        while (elapsedTime < dissolveDuration)
        {
            float dissolveValue = Mathf.Lerp(1, 0, elapsedTime / dissolveDuration);
            if (dissolveMaterial != null)
            {
                dissolveMaterial.SetFloat("_Dissolve", dissolveValue);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (dissolveMaterial != null)
        {
            dissolveMaterial.SetFloat("_Dissolve", 0);
        }
    }
}