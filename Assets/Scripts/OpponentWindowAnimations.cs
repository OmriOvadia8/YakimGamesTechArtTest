using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpponentWindowAnimations : MonoBehaviour
{
    public float scaleFactor = 0.8f;      // Factor to scale down
    public float overshoot = 1.2f;        // Factor to scale up beyond original size
    public float duration = 0.5f;         // Duration for each scale animation
    public float cooldownDuration = 1.0f; // Duration of the cooldown between loops

    private Vector3 originalScale;        // To store the original scale

    void Start()
    {
        originalScale = transform.localScale;

        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        // Scale down
        sequence.Append(transform.DOScale(originalScale * scaleFactor, duration).SetEase(Ease.OutQuad));

        // Scale up beyond original
        sequence.Append(transform.DOScale(originalScale * overshoot, duration).SetEase(Ease.OutQuad));

        // Return to original scale
        sequence.Append(transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad));

        // Cooldown period
        sequence.AppendInterval(cooldownDuration);

        // Set the sequence to loop
        sequence.SetLoops(-1); // -1 for infinite loops
    }
}
