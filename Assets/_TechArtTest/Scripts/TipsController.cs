using DG.Tweening;
using UnityEngine;

public class TipsController : MonoBehaviour
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public float duration = 0.5f; // Total duration of the animation

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Calculate the target position
        Vector2 targetPosition = rectTransform.anchoredPosition - new Vector2(0, 40);

        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        // Move the GameObject down by 40 units and fade out
        sequence.Append(rectTransform.DOAnchorPosY(targetPosition.y, duration).SetEase(Ease.InBack));
        sequence.Join(canvasGroup.DOFade(0, duration)); // Fades out to 0

        // Optional: Disable the GameObject after the animation
        sequence.OnComplete(() => gameObject.SetActive(false));
    }

    private void MoveToCenter() // animation to make the tips go to center as the video shows (not implemented according to the Word file)
    {
        rectTransform = GetComponent<RectTransform>();

        // Calculate target position
        float targetPosX = rectTransform.anchoredPosition.x - 220;

        // Move the GameObject
        rectTransform.DOAnchorPosX(targetPosX, duration).SetEase(Ease.InQuad); 
    }
}
