using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VSScreenMove : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image fillImage;

    [Header("Animation Settings")]
    [SerializeField] private float moveDistance = 700f;
    [SerializeField] private float overshootDistance = 100f;
    [SerializeField] private float easeInDuration = 0.5f;
    [SerializeField] private float easeOutDuration = 1.0f;

    private Vector2 originalPosition;

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        CreateAnimationSequence();
    }

    private void CreateAnimationSequence()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOAnchorPosX(GetTargetPosition(-moveDistance - overshootDistance), easeInDuration).SetEase(Ease.OutQuad));
        sequence.Join(fillImage.DOFillAmount(1, easeInDuration));
        sequence.Append(rectTransform.DOAnchorPosX(GetTargetPosition(-moveDistance), easeOutDuration).SetEase(Ease.OutCubic));
        sequence.AppendCallback(() => fillImage.fillOrigin = 0);
        sequence.Append(fillImage.DOFillAmount(0, easeOutDuration));
    }

    private float GetTargetPosition(float offset)
    {
        return originalPosition.x + offset;
    }
}
