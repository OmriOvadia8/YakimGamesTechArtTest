using DG.Tweening;
using UnityEngine;

public class EnemyDetailsAnimations : MonoBehaviour
{
    [Header("Enemy UI Elements")]
    [SerializeField] private GameObject enemyName;
    [SerializeField] private CanvasGroup nameCanvasGroup;
    [SerializeField] private GameObject enemyLevel;
    [SerializeField] private CanvasGroup levelCanvasGroup;

    [Header("General Animation Parameters")]
    [SerializeField] private float startingScale = 0.3f;
    [SerializeField] private float midScaleDuration = 0.5f;
    [SerializeField] private float finalScaleDuration = 0.3f;


    [Header("Name Animation Parameters")]
    [SerializeField] private float nameMidScale = 0.6f;
    [SerializeField] private float nameFinalScale = 0.5f;


    [Header("Level Animation Parameters")]
    [SerializeField] private float levelMidScale = 0.5f;
    [SerializeField] private float levelFinalScale = 0.4f;

    private void Start()
    {
        InitGameObjectAnimation(nameCanvasGroup, nameMidScale, nameFinalScale);
        InitGameObjectAnimation(levelCanvasGroup, levelMidScale, levelFinalScale);
    }

    private void InitGameObjectAnimation(CanvasGroup canvasGroup, float midScale, float finalScale)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.transform.localScale = Vector3.one * startingScale;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.transform.DOScale(midScale, midScaleDuration).SetEase(Ease.OutBack));
        mySequence.Join(canvasGroup.DOFade(1f, 0.5f));
        mySequence.Append(canvasGroup.transform.DOScale(finalScale, finalScaleDuration).SetEase(Ease.OutSine));
    }
}
