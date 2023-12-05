using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VSScreenMove : MonoBehaviour
{
    public float moveDistance = 700f;    // Final move distance
    public float overshootDistance = 100f; // Additional distance for overshoot
    public float durationFast = 0.5f;    // Duration for the fast movement
    public float durationSlow = 1.0f;    // Duration for the slower movement

    public Image fillImage; // Assign this in the inspector

    private Vector2 originalPosition;    // To store the original position

    public void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        // Set the initial fill origin to top (assuming top is 1 for vertical fill method)
        fillImage.fillOrigin = 1; // Change this value based on your fill method

        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        // Quick movement to the overshoot position
        sequence.Append(rectTransform.DOAnchorPosX(originalPosition.x - moveDistance - overshootDistance, durationFast).SetEase(Ease.OutQuad));

        // Simultaneously, fill the image from 0 to 1 quickly
        sequence.Join(fillImage.DOFillAmount(1, durationFast));

        // Slower movement to the final position
        sequence.Append(rectTransform.DOAnchorPosX(originalPosition.x - moveDistance, durationSlow).SetEase(Ease.OutCubic));

        // Change fill origin and then empty the fill from 1 to 0
        sequence.AppendCallback(() => fillImage.fillOrigin = 0); // Assuming bottom is 0 for vertical fill method
        sequence.Append(fillImage.DOFillAmount(0, durationSlow));
    }
}
