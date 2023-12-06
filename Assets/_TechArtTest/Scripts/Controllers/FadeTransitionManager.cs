using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FadeTransitionManager : MonoBehaviour
{
    [Header("Transition Components")]
    [SerializeField] private MagnifyingGlassAnimation magnifyingGlass;
    [SerializeField] private PlayButtonAnim playButtonAnim;

    [Header("UI Elements")]
    [SerializeField] private CanvasGroup[] canvasGroups;

    [Header("Transition Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float moveDistance = 40f;

    private RectTransform[] rectTransforms;

    void Awake()
    {
        rectTransforms = new RectTransform[canvasGroups.Length];
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            rectTransforms[i] = canvasGroups[i].GetComponent<RectTransform>();
        }
    }

    public void FadeAndDisappear()
    {
        magnifyingGlass.ShrinkAndDisappear();

        for (int i = 0; i < canvasGroups.Length; i++)
        {
            var canvasGroup = canvasGroups[i];
            var rectTransform = rectTransforms[i];
            if (rectTransform == null) continue;

            Vector2 targetPosition = rectTransform.anchoredPosition - new Vector2(0, moveDistance);
            CreateFadeSequence(canvasGroup, rectTransform, targetPosition, fadeDuration, false);
        }
    }

    public void AppearAndRise()
    {
        magnifyingGlass.GrowAndAppear();
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            var canvasGroup = canvasGroups[i];
            var rectTransform = rectTransforms[i];
            if (rectTransform == null) continue;

            Vector2 targetPosition = rectTransform.anchoredPosition + new Vector2(0, moveDistance);
            CreateFadeSequence(canvasGroup, rectTransform, targetPosition, fadeDuration, true);
        }
    }

    private void CreateFadeSequence(CanvasGroup canvasGroup, RectTransform rectTransform, Vector2 targetPosition, float duration, bool fadeIn)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(targetPosition.y, duration).SetEase(fadeIn ? Ease.OutBack : Ease.InBack));
        sequence.Join(canvasGroup.DOFade(fadeIn ? 1 : 0, duration));
        if (fadeIn) sequence.OnStart(() => canvasGroup.gameObject.SetActive(true));
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
