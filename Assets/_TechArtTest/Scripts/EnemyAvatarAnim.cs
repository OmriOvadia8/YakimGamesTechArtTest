using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAvatarAnim : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] Image image;

    [SerializeField] string glowShaderProperty = "_Glow";
    private Material material; // For the glow effect
    private RectTransform rectTransform; 

    [Header("Animation Settings")]
    [SerializeField] private Vector2 moveOffset = new (400, 0); 
    [SerializeField] private float moveDuration = 1f; 
    [SerializeField] private float colorTransitionDuration = 0.75f;
    [SerializeField] private Vector4 glowColorHSVCode = new (60, 40, 100, 0);
    [SerializeField] private Vector4 normalColorHSVCode = new (60, 0, 0, 0);

    void Start()
    {
        material = image.material;
        material.SetColor(glowShaderProperty, Extensions.ColorFromHSV(glowColorHSVCode));
        rectTransform = image.rectTransform;

        Vector2 targetPosition = rectTransform.anchoredPosition + moveOffset;

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(rectTransform.DOAnchorPos(targetPosition, moveDuration).SetEase(Ease.OutQuad));

        mySequence.Join(DOTween.To(() => material.GetColor(glowShaderProperty),
                                   x => material.SetColor(glowShaderProperty, x),
                                   Extensions.ColorFromHSV(normalColorHSVCode),
                                   colorTransitionDuration));
    }
}
