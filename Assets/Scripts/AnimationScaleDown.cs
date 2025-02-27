using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationScaleDown : MonoBehaviour
{
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnDisable()
    {
        transform.DOScale(0.02f, 0.3f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() => gameObject.SetActive(false));
    }

    void OnEnable()
    {
        transform.localScale = originalScale; // Reset scale when re-enabled
    }
}
