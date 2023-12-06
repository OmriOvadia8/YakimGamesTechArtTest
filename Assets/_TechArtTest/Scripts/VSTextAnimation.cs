using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSTextAnimation : MonoBehaviour
{
    public Image image; // Assign this in the inspector

    private Material material; // For the glow effect
    private RectTransform rectTransform; // For scaling

    void Start()
    {
        // Initialize material, color, and scale
        material = image.material;
        material.SetColor("_Glow", ColorFromHSV(0, 0, 1, 0)); // White with alpha 0
        image.color = new Color(1, 1, 1, 0.5f); // White with alpha 0.5
        rectTransform = image.rectTransform;
        rectTransform.localScale = Vector3.one * 1.3f;

        // Create a DOTween sequence with initial delay
        Sequence mySequence = DOTween.Sequence();

        // Start with a delay
        mySequence.AppendInterval(0.55f);

        // Activate the GameObject after the delay
        mySequence.AppendCallback(() => image.gameObject.SetActive(true));

        // Rest of the animation sequence
        // Animate the image color alpha to 1 over 0.25 seconds
        mySequence.Append(image.DOFade(1f, 0.25f));

        // Simultaneously scale down to 0.4 over 0.5 seconds
        mySequence.Join(rectTransform.DOScale(0.4f, 0.25f).SetEase(Ease.OutQuad));

        // Start changing the glow color when the scale reaches 0.4
        mySequence.AppendCallback(() => material.SetColor("_Glow", ColorFromHSV(0, 0, 60, 0)));

        // Scale up to 0.6 while gradually changing the glow color
        mySequence.Append(rectTransform.DOScale(0.6f, 0.25f));
        mySequence.Join(DOTween.To(() => material.GetColor("_Glow"),
                                    x => material.SetColor("_Glow", x),
                                    ColorFromHSV(0, 0, 0, 0), // Target color HSV (0,0,0) with alpha 0
                                    0.5f)); // Duration
    }

    Color ColorFromHSV(float h, float s, float v, float a)
    {
        Color color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);
        color.a = a;
        return color;
    }
}
