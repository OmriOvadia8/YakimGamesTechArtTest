using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSScreenMove : MonoBehaviour
{
    public float moveDistance = 700f;   // Final move distance
    public float overshootDistance = 100f; // Additional distance for overshoot
    public float durationFast = 0.5f;   // Duration for the fast movement
    public float durationSlow = 1.0f;   // Duration for the slower movement

    private Vector2 originalPosition;   // To store the original position

    public void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        // Quick movement to the overshoot position
        sequence.Append(rectTransform.DOAnchorPosX(originalPosition.x - moveDistance - overshootDistance, durationFast).SetEase(Ease.OutQuad));

        // Slower movement to the final position
        sequence.Append(rectTransform.DOAnchorPosX(originalPosition.x - moveDistance, durationSlow).SetEase(Ease.OutCubic));
    }
}
