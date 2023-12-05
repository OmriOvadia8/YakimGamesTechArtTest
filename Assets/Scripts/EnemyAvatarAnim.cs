using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAvatarAnim : MonoBehaviour
{
    public Image image; // Assign this in the inspector

    private Material material; // For the glow effect
    private RectTransform rectTransform; // For moving the image

    void Start()
    {
        // Initialize material, position, and color
        material = image.material;
        material.SetColor("_Glow", ColorFromHSV(60, 40, 100, 0)); // Initial glow color
        rectTransform = image.rectTransform;

        // Determine the target position to move left by 400 units
        Vector2 targetPosition = rectTransform.anchoredPosition + new Vector2(400, 0);

        // Create a DOTween sequence
        Sequence mySequence = DOTween.Sequence();

        // Move the image to the left by 400 units with a smooth ease out
        mySequence.Append(rectTransform.DOAnchorPos(targetPosition, 1f).SetEase(Ease.OutQuad));

        // Change the glow color simultaneously but complete it faster
        mySequence.Join(DOTween.To(() => material.GetColor("_Glow"),
                                   x => material.SetColor("_Glow", x),
                                   ColorFromHSV(60, 0, 0, 0), // Target glow color
                                   0.75f)); // Faster duration for the color transition
    }

    Color ColorFromHSV(float h, float s, float v, float a)
    {
        Color color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);
        color.a = a;
        return color;
    }
}
