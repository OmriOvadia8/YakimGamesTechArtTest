using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private Vector2 scrollingSpeed;

    private void Start() => InitializeScreenScrolling();

    private void InitializeScreenScrolling()
    {
        DOTween.To(() => 0f, t => UpdateUVRect(t), float.MaxValue, float.MaxValue)
       .SetEase(Ease.Linear)
       .SetLoops(-1, LoopType.Incremental);
    }

    private void UpdateUVRect(float t)
    {
        Vector2 newOffset = new(t * scrollingSpeed.x, t * scrollingSpeed.y);
        backgroundImage.uvRect = new Rect(newOffset, backgroundImage.uvRect.size);
    }
}
