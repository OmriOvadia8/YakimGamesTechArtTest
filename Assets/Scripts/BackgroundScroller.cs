using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private Vector2 _speed;

    private void Start()
    {
        DOTween.To(() => 0f, t => UpdateUVRect(t), float.MaxValue, float.MaxValue)
               .SetEase(Ease.Linear)
               .SetLoops(-1, LoopType.Incremental);
    }

    private void UpdateUVRect(float t)
    {
        Vector2 newOffset = new Vector2(t * _speed.x, t * _speed.y);
        _img.uvRect = new Rect(newOffset, _img.uvRect.size);
    }
}
