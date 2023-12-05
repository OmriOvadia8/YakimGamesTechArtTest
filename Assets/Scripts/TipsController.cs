using DG.Tweening;
using UnityEngine;

public class TipsController : MonoBehaviour
{
    private RectTransform rectTransform;
    public float duration = 0.5f; // Total duration of the animation

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Calculate target position
        float targetPosX = rectTransform.anchoredPosition.x - 220;

        // Move the GameObject
        rectTransform.DOAnchorPosX(targetPosX, duration).SetEase(Ease.InQuad); // Or Ease.InSine
    }
}
