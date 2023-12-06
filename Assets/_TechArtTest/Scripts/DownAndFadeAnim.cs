using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DownAndFadeAnim : MonoBehaviour
{
    [SerializeField] MagnifyingGlassAnimation magnifyingGlass;
    [SerializeField] PlayButtonAnim playButtonAnim;
    [SerializeField] private CanvasGroup[] canvasGroups; // Array of CanvasGroup components
    public float duration = 0.5f; // Total duration of the animation
    public float moveDistance = 40f; // Distance to move down

    //private void Start()
    //{
    //    FadeAndDisappear();
    //    Invoke("AppearAndRise", 2f);
    //}

    public void FadeAndDisappear()
    {
        magnifyingGlass.ShrinkAndDisappear();

        foreach (var canvasGroup in canvasGroups)
        {
            RectTransform rectTransform = canvasGroup.GetComponent<RectTransform>();
            if (rectTransform == null) continue;

            // Calculate the target position
            Vector2 targetPosition = rectTransform.anchoredPosition - new Vector2(0, moveDistance);

            // Create a sequence for each CanvasGroup
            Sequence sequence = DOTween.Sequence();

            // Move the GameObject down by moveDistance units and fade out
            sequence.Append(rectTransform.DOAnchorPosY(targetPosition.y, duration).SetEase(Ease.InBack));
            sequence.Join(canvasGroup.DOFade(0, duration)); // Fades out to 0
        }
    }

    public void AppearAndRise()
    {
        magnifyingGlass.GrowAndAppear();
        foreach (var canvasGroup in canvasGroups)
        {
            RectTransform rectTransform = canvasGroup.GetComponent<RectTransform>();
            if (rectTransform == null) continue;

            // Calculate the target position to move back to original position
            Vector2 targetPosition = rectTransform.anchoredPosition + new Vector2(0, moveDistance);

            // Create a sequence for each CanvasGroup
            Sequence sequence = DOTween.Sequence();

            // Move the GameObject up by moveDistance units and fade in
            sequence.Append(rectTransform.DOAnchorPosY(targetPosition.y, duration).SetEase(Ease.OutBack));
            sequence.Join(canvasGroup.DOFade(1, duration)); // Fades in to 1

            // Optional: Enable the GameObject before the animation starts
            sequence.OnStart(() => canvasGroup.gameObject.SetActive(true));
        }
    }

    public void ActivatePlayButton(bool isActive, float transitionTime)
    {
        StartCoroutine(ActivatePlayButtonWithDelay(isActive, transitionTime));
    }

    private IEnumerator ActivatePlayButtonWithDelay(bool isActive, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isActive)
        {
            playButtonAnim.GrowAndAppear();
        }
        else
        {
            playButtonAnim.ShrinkAndDisappear();
        }
    }


    private void MoveToCenter() // animation to make the tips go to center as the video shows (not implemented according to the Word file, just showing animation logic)
    {
        //rectTransform = GetComponent<RectTransform>();
        //float targetPosX = rectTransform.anchoredPosition.x - targetPos;
        //rectTransform.DOAnchorPosX(targetPosX, duration).SetEase(Ease.InQuad); 
    }
}
