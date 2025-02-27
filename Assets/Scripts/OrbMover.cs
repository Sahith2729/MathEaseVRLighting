using UnityEngine;
using DG.Tweening;

public class OrbMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveDuration = 3f;
    [SerializeField] private float floatAmount = 0.3f;
    [SerializeField] private float floatDuration = 3f; // Updated to 3 seconds

    private Tween floatTween; // Store the yoyo tween

    private void Start()
    {
        StartYoyoAnimation(); // Start the default yoyo animation
    }

    private void StartYoyoAnimation()
    {
        // Ensure no duplicate tweens
        floatTween?.Kill();

        // Start the yoyo animation (floating effect)
        floatTween = transform.DOMoveY(transform.position.y + floatAmount, floatDuration)
            .SetRelative(true)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void MoveToTarget()
    {
        if (target == null) return;

        // Kill the floating animation before moving
        floatTween?.Kill();

        // Move to target, then restart yoyo animation
        transform.DOMove(target.position, moveDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => StartYoyoAnimation());
    }
}
