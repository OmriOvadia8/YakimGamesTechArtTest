using DG.Tweening;
using UnityEngine;

public class PlayButtonAnim : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform rectTransform;

    public void ShrinkAndDisappear()
    {
        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector3.zero;
    }

    public void GrowAndAppear()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOScale(Vector3.one * 1.1f, 0.3f).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, 0.3f));

        sequence.Append(rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack));
    }
}
