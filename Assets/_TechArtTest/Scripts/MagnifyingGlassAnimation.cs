using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MagnifyingGlassAnimation : MonoBehaviour
{
    public float duration = 2.0f; // Duration for one circle loop
    public float radius = 100f;   // Radius of the circle in UI units
    private Tween rotatingTween;   // Reference to the rotating tween
    private Image image;           // Reference to the image component
    private RectTransform rectTransform; // Reference to the RectTransform
    private Vector2 originalPosition; // To store the original position
    private Vector3 originalScale; // To store the original scale

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        if (image == null || rectTransform == null)
        {
            Debug.LogError("Required component not found!");
            return;
        }

        // Store the original position and scale
        originalPosition = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;

        StartRotating();
    }

    private void StartRotating()
    {
        // Create and store a tween for a smooth circular motion
        rotatingTween = DOTween.To(() => 0f, x => rectTransform.anchoredPosition = originalPosition + new Vector2(Mathf.Cos(x) * radius, Mathf.Sin(x) * radius), 2 * Mathf.PI, duration)
                               .SetLoops(-1, LoopType.Restart)
                               .SetEase(Ease.Linear);
    }

    public void ShrinkAndDisappear()
    {
        // Stop the rotation animation and reset position
        rotatingTween?.Kill();
        rectTransform.anchoredPosition = originalPosition;

        // Set initial scale to the original for starting the shrink animation
        rectTransform.localScale = originalScale;

        // Create a sequence for the shrinking and fading animation
        Sequence sequence = DOTween.Sequence();

        // Animate to grow a little bit first
        sequence.Append(rectTransform.DOScale(originalScale * 1.2f, 0.2f).SetEase(Ease.OutBack));

        // After growing, start shrinking and fading out simultaneously
        sequence.Append(rectTransform.DOScale(0f, 0.3f).SetEase(Ease.InBack))
                .Join(image.DOFade(0f, 0.3f)); // Match the duration of fading out with shrinking
    }

    public void GrowAndAppear()
    {
        // Reset to original position and scale
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localScale = Vector3.zero;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        // Create a sequence for the growing and appearing animation
        Sequence sequence = DOTween.Sequence();

        // Animate to grow to a little bit larger than normal and fade in simultaneously
        sequence.Append(rectTransform.DOScale(originalScale * 1.2f, 0.3f).SetEase(Ease.OutBack))
                .Join(image.DOFade(1f, 0.3f)); // Start fading in at the same time

        // Then scale back to normal size
        sequence.Append(rectTransform.DOScale(originalScale, 0.2f).SetEase(Ease.InBack));

        // Optional: Restart the rotation animation
        sequence.OnComplete(StartRotating);
    }


}
