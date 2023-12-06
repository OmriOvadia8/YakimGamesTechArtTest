using UnityEngine;
using DG.Tweening;

public class CancelButtonControl : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float scaleFactor = 0.8f;     
    [SerializeField] private float overshoot = 1.2f;        
    [SerializeField] private float duration = 0.5f;         
    [SerializeField] private float cooldownDuration = 1.0f; 

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        StartScalingAnimation();
    }

    private void StartScalingAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(originalScale * scaleFactor, duration).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOScale(originalScale * overshoot, duration).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad));
        sequence.AppendInterval(cooldownDuration);

        sequence.SetLoops(-1);
    }
}
