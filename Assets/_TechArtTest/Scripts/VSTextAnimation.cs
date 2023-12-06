using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VSTextAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private string glowShaderProperty = "_Glow";

    [Header("Glow Color")]
    [SerializeField] private Vector4 initialGlowColor = new(0, 0, 1, 0);
    [SerializeField] private Vector4 finalGlowColor = new(0, 0, 60, 0);
    [SerializeField] private Color startingColor;

    [Header("Timers Settings")]
    [SerializeField] private float initialDelay = 0.55f;
    [SerializeField] private float fadeInDuration = 0.25f;
    [SerializeField] private float scaleInDuration = 0.25f;
    [SerializeField] private float scaleOutDuration = 0.25f;
    [SerializeField] private float glowFadeOutDuration = 0.5f;

    [Header("Scaling Settings")]
    [SerializeField] private float startingScale = 1.3f;
    [SerializeField] private float midScale = 0.4f;
    [SerializeField] private float finalScale = 0.6f;

    private Material material;
    private RectTransform rectTransform;

    private void Start()
    {
        material = image.material;
        material.SetColor(glowShaderProperty, Extensions.ColorFromHSV(initialGlowColor));

        image.color = startingColor;

        rectTransform = image.rectTransform;
        rectTransform.localScale = Vector3.one * startingScale;

        Sequence mySequence = DOTween.Sequence()
            .AppendInterval(initialDelay)
            .AppendCallback(() => image.gameObject.SetActive(true))
            .Append(image.DOFade(1f, fadeInDuration))
            .Join(rectTransform.DOScale(midScale, scaleInDuration).SetEase(Ease.OutQuad))
            .AppendCallback(() => material.SetColor(glowShaderProperty, Extensions.ColorFromHSV(finalGlowColor)))
            .Append(rectTransform.DOScale(finalScale, scaleOutDuration))
            .Join(DOTween.To(() => material.GetColor(glowShaderProperty), x => material.SetColor(glowShaderProperty, x), Extensions.ColorFromHSV(Vector4.zero), glowFadeOutDuration));
    }
}
