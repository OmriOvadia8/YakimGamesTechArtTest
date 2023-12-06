using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MagnifyingGlassAnimation : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;

    [Header("Animation Settings")]
    [SerializeField] private float rotationDuration = 2.0f;
    [SerializeField] private float rotationRadius = 100f;
    [SerializeField] private float scaleUpDuration = 0.2f;
    [SerializeField] private float scaleDownDuration = 0.3f;
    [SerializeField] private float growScaleFactor = 1.2f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float originalScaleResetDuration = 0.2f;

    private Tween rotatingTween;
    private Vector2 originalPosition;
    private Vector3 originalScale;

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;

        StartRotating();
    }

    private void StartRotating()
    {
        rotatingTween = DOTween.To(() => 0f, x => rectTransform.anchoredPosition = originalPosition + new Vector2(Mathf.Cos(x) * rotationRadius, Mathf.Sin(x) * rotationRadius), 2 * Mathf.PI, rotationDuration)
                               .SetLoops(-1, LoopType.Restart)
                               .SetEase(Ease.Linear);
    }

    public void ShrinkAndDisappear()
    {
        rotatingTween?.Kill();
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localScale = originalScale;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOScale(originalScale * growScaleFactor, scaleUpDuration).SetEase(Ease.OutBack));
        sequence.Append(rectTransform.DOScale(0, scaleDownDuration).SetEase(Ease.InBack))
                .Join(image.DOFade(0f, fadeDuration));
    }

    public void GrowAndAppear()
    {
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localScale = Vector3.zero;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOScale(originalScale * growScaleFactor, scaleUpDuration).SetEase(Ease.OutBack))
                .Join(image.DOFade(1f, fadeDuration));
        sequence.Append(rectTransform.DOScale(originalScale, originalScaleResetDuration).SetEase(Ease.InBack));
        sequence.OnComplete(StartRotating);
    }
}
