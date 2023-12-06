using DG.Tweening;
using UnityEngine;

public class PlayButtonAnim : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float growScaleFactor = 1.1f;
    [SerializeField] private float growDuration = 0.3f;
    [SerializeField] private float shrinkDuration = 0.2f;

    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;

    public void ShrinkAndDisappear()
    {
        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector3.zero;
    }

    public void GrowAndAppear()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOScale(Vector3.one * growScaleFactor, growDuration).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, growDuration))
                .Append(rectTransform.DOScale(Vector3.one, shrinkDuration).SetEase(Ease.InBack));
    }
}
