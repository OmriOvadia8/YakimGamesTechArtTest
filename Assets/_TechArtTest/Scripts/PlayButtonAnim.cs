using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAnim : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform rectTransform;

    public void ShrinkAndDisappear()
    {
        //// Create a sequence for the shrinking and fading animation
        //Sequence sequence = DOTween.Sequence();

        //// Animate to grow a little bit first
        //sequence.Append(rectTransform.DOScale(Vector3.one * 1.1f, 0.1f).SetEase(Ease.OutBack));

        //// After growing, start shrinking and fading out simultaneously
        //sequence.Append(rectTransform.DOScale(0f, 0.4f).SetEase(Ease.InBack))
        //        .Join(canvasGroup.DOFade(0f, 0.4f)); // Match the duration of fading out with shrinking

        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector3.zero;
    }

    public void GrowAndAppear()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOScale(Vector3.one * 1.1f, 0.3f).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, 0.3f)); // Start fading in at the same time

        // Then scale back to normal size
        sequence.Append(rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack));

    }
}
