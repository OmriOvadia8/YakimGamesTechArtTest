using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetailsAnim : MonoBehaviour
{
    [SerializeField] GameObject enemyName;
    [SerializeField] CanvasGroup nameCanvasGroup;
    [SerializeField] GameObject enemyLevel;
    [SerializeField] CanvasGroup levelCanvasGroup;

    void Start()
    {
        InitGameObjectAnimation(nameCanvasGroup, 0.6f, 0.5f);
        InitGameObjectAnimation(levelCanvasGroup, 0.5f, 0.4f);
    }

    void InitGameObjectAnimation(CanvasGroup canvasGroup, float midScale ,float finalScale)
    {
        // Set initial alpha and scale
        canvasGroup.alpha = 0.5f;
        canvasGroup.transform.localScale = Vector3.one * 0.3f;

        // Create a DOTween sequence for the animation
        Sequence mySequence = DOTween.Sequence();

        // Animate scale up to 1.1
        mySequence.Append(canvasGroup.transform.DOScale(midScale, 0.5f).SetEase(Ease.OutBack)); // Duration and ease are adjustable

        // Simultaneously animate alpha to 1
        mySequence.Join(canvasGroup.DOFade(1f, 0.5f)); // Ensure this duration matches the scale up

        // Then animate scale back down to 1
        mySequence.Append(canvasGroup.transform.DOScale(finalScale, 0.3f).SetEase(Ease.OutSine)); // Duration and ease are adjustable
    }
}
