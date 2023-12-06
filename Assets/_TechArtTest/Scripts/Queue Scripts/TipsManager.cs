using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text tipText;

    [Header("Tip Settings")]
    [SerializeField] Tip[] tips;
    [SerializeField] int tips_switching_time_in_seconds = 5;

    [Header("Animation Settings")]
    [SerializeField] float transitionDuration = 0.5f;

    private int lastTipSerialNumber = -1;

    void Start() => InitializeTips();

    private void InitializeTips()
    {
        if (tips.Length > 1)
        {
            StartCoroutine(RotateTips());
        }
        else if (tips.Length == 1)
        {
            tipText.text = tips[0].tip_text;
        }
    }

    IEnumerator RotateTips()
    {
        while (true)
        {
            yield return tipText.DOFade(0, transitionDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
            int nextTipSerialNumber = GetNextTipSerialNumber();
            Tip nextTip = GetTipBySerialNumber(nextTipSerialNumber);
            tipText.text = nextTip.tip_text;
            lastTipSerialNumber = nextTipSerialNumber;
            yield return tipText.DOFade(1, transitionDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
            yield return new WaitForSeconds(tips_switching_time_in_seconds);
        }
    }

    private int GetNextTipSerialNumber()
    {
        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, tips.Length);
        } while (tips.Length > 1 && tips[nextIndex].vs_screen_tip_number == lastTipSerialNumber);

        return tips[nextIndex].vs_screen_tip_number;
    }

    private Tip GetTipBySerialNumber(int serialNumber)
    {
        foreach (Tip tip in tips)
        {
            if (tip.vs_screen_tip_number == serialNumber)
                return tip;
        }
        return null;
    }
}

[System.Serializable]
public class Tip // values will be assigned via server (for this mockup it can be assigned in the inspector)
{
    public int vs_screen_tip_number;
    public string tip_text;
}
