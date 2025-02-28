using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationScaleDown : MonoBehaviour
{
    private Vector3 originalScale;

    [SerializeField] private float scaleDownValue = 0.02f; // Value to scale down to
    [SerializeField] private float duration = 0.3f; // Duration of the scale-down animation

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnDisable()
    {
        transform.DOScale(scaleDownValue, duration)
            .SetEase(Ease.OutElastic)
            .OnComplete(() => gameObject.SetActive(false));
    }

    void OnEnable()
    {
        transform.localScale = originalScale; // Reset scale when re-enabled
    }
}