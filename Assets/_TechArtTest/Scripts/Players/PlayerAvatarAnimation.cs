using UnityEngine;
using DG.Tweening;

public class PlayerAvatarAnimation : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject vsScreen;
    [SerializeField] private RectTransform imageRectTransform;

    [Header("Animation Settings")]
    [SerializeField] private float rightOffset = 10f;
    [SerializeField] private float leftOffset = 140f;
    [SerializeField] private float finalOffset = 120f;
    [SerializeField] private float duration = 0.5f;

    private Vector2 originalPosition;

    private void Start() => originalPosition = imageRectTransform.anchoredPosition;

    public void TransitionToVSScreen()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x + rightOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - leftOffset, originalPosition.y), duration).SetEase(Ease.InOutQuad));
        sequence.Insert(duration, DOTween.Sequence().OnComplete(() => vsScreen.SetActive(true)));
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - finalOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));
    }
}
