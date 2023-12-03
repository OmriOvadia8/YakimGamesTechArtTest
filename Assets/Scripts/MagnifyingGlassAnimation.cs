using UnityEngine;
using DG.Tweening;

public class MagnifyingGlassAnimation : MonoBehaviour
{
    public float duration = 2.0f; // Duration for one circle loop
    public float radius = 100f;   // Radius of the circle in UI units

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;

        // Create a tween for a smooth circular motion
        DOTween.To(() => 0f, x => rectTransform.anchoredPosition = startPosition + new Vector2(Mathf.Cos(x) * radius, Mathf.Sin(x) * radius), 2 * Mathf.PI, duration)
               .SetLoops(-1, LoopType.Restart)
               .SetEase(Ease.Linear);
    }
}
