using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAnim : MonoBehaviour
{
    [SerializeField] GameObject vsScreen;
    public RectTransform imageRectTransform; // Assign your UI Image's RectTransform
    public float rightOffset = 10f;          // Amount to move to the right
    public float leftOffset = 140f;          // Amount to move to the left
    public float finalOffset = 120f;         // Final offset from the original position
    public float duration = 0.5f;            // Duration for each movement

    private Vector2 originalPosition;        // To store the original position

    void Start()
    {
        if (imageRectTransform == null)
        {
            imageRectTransform = GetComponent<RectTransform>();
        }

        originalPosition = imageRectTransform.anchoredPosition;


    }

    public void TransitionToVSScreen()
    {
        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        // Move slightly to the right
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x + rightOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));

        // Insert a callback in the middle of the sequence
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - leftOffset, originalPosition.y), duration).SetEase(Ease.InOutQuad));
        sequence.Insert(duration, DOTween.Sequence().OnComplete(() => vsScreen.SetActive(true)));  // Activate vsScreen here

        // Continue with the rest of the CharacterAnim sequence
        sequence.Append(imageRectTransform.DOAnchorPos(new Vector2(originalPosition.x - finalOffset, originalPosition.y), duration).SetEase(Ease.OutQuad));
    }
}
