using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    public TMP_Text tipText; // UI element to display the tip
    public int tipsSwitchingTimeInSeconds = 5; // Time each tip is displayed
    public Tip[] tips; // Array of tips with serial numbers
    private float transitionDuration = 0.5f; // Duration of the transition effect
    private int lastTipSerialNumber = -1; // Serial number of the last displayed tip

    void Start()
    {
        if (tips.Length > 1)
        {
            StartCoroutine(RotateTips());
        }
        else if (tips.Length == 1)
        {
            tipText.text = tips[0].Text;
        }
    }

    IEnumerator RotateTips()
    {
        while (true)
        {
            yield return tipText.DOFade(0, transitionDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
            int nextTipSerialNumber = GetNextTipSerialNumber();
            Tip nextTip = GetTipBySerialNumber(nextTipSerialNumber);
            tipText.text = nextTip.Text;
            lastTipSerialNumber = nextTipSerialNumber;
            yield return tipText.DOFade(1, transitionDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
            yield return new WaitForSeconds(tipsSwitchingTimeInSeconds);
        }
    }

    private int GetNextTipSerialNumber()
    {
        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, tips.Length);
        } while (tips.Length > 1 && tips[nextIndex].SerialNumber == lastTipSerialNumber);

        return tips[nextIndex].SerialNumber;
    }

    private Tip GetTipBySerialNumber(int serialNumber)
    {
        foreach (Tip tip in tips)
        {
            if (tip.SerialNumber == serialNumber)
                return tip;
        }
        return null;
    }
}


[System.Serializable]
public class Tip
{
    public int SerialNumber;
    public string Text;
}