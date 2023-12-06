using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VSTextAnimation : MonoBehaviour
{
    public Image image;
    private Material material; 
    private RectTransform rectTransform; 

    void Start()
    {
        material = image.material;
        material.SetColor("_Glow", ColorFromHSV(0, 0, 1, 0)); 
        image.color = new Color(1, 1, 1, 0.5f); 
        rectTransform = image.rectTransform;
        rectTransform.localScale = Vector3.one * 1.3f;

        Sequence mySequence = DOTween.Sequence();

        mySequence.AppendInterval(0.55f);

        mySequence.AppendCallback(() => image.gameObject.SetActive(true));

        mySequence.Append(image.DOFade(1f, 0.25f));

        mySequence.Join(rectTransform.DOScale(0.4f, 0.25f).SetEase(Ease.OutQuad));

        mySequence.AppendCallback(() => material.SetColor("_Glow", ColorFromHSV(0, 0, 60, 0)));

        mySequence.Append(rectTransform.DOScale(0.6f, 0.25f));
        mySequence.Join(DOTween.To(() => material.GetColor("_Glow"),
                                    x => material.SetColor("_Glow", x),
                                    ColorFromHSV(0, 0, 0, 0),
                                    0.5f));
    }

    Color ColorFromHSV(float h, float s, float v, float a)
    {
        Color color = Color.HSVToRGB(h / 360f, s / 100f, v / 100f);
        color.a = a;
        return color;
    }
}
