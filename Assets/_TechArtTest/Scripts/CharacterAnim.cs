using UnityEngine;
using DG.Tweening;

public class CharacterAnim : MonoBehaviour
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

    private void Start()
    {
        // Initialize RectTransform if not assigned
        if (imageRectTransform == null)
        {
            imageRectTransform = GetComponent<RectTransform>();
        }

        // Store the original position for reference
        originalPosition = imageRectTransform.anchoredPosition;
    }

    public void TransitionToVSScreen()
    {
        // Create and configure the animation sequence
        Sequence sequence = DOTween.Sequence();

        // Move slightly to the right
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x + rightOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));

        // Then move to the left
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - leftOffset, originalPosition.y), duration).SetEase(Ease.InOutQuad));

        // Activate vsScreen at the midpoint of the sequence
        sequence.Insert(duration, DOTween.Sequence().OnComplete(() => vsScreen.SetActive(true)));

        // Continue with the final movement
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - finalOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));
    }
}
