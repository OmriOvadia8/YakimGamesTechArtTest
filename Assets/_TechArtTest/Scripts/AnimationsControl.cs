using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControl : MonoBehaviour
{
    [SerializeField] GameObject waitingScreen;
    [SerializeField] GameObject vsScreen;
    [SerializeField] GameObject waitingText;
    [SerializeField] GameObject playButton;
    [SerializeField] DownAndFadeAnim detailsFading;
    [SerializeField] float waitingDuration;
    [SerializeField] CharacterAnim characterAnim;
    private Coroutine matchFoundCoroutine;

    private void Start()
    {
        vsScreen.SetActive(false);
        matchFoundCoroutine = StartCoroutine(DelayedMatchFound());
    }

    public void StartFindingMatch()
    {
        waitingText.SetActive(true);
        detailsFading.ActivatePlayButton(false, 0f);
        detailsFading.AppearAndRise();
        matchFoundCoroutine = StartCoroutine(DelayedMatchFound());
    }


    public void StopFindingMatch()
    {
        if (matchFoundCoroutine != null)
        {
            StopCoroutine(matchFoundCoroutine);
        }
        detailsFading.FadeAndDisappear();
        detailsFading.ActivatePlayButton(true, 0.5f);
    }

    private IEnumerator DelayedMatchFound()
    {
        yield return new WaitForSeconds(waitingDuration);
        detailsFading.FadeAndDisappear();
        yield return new WaitForSeconds(waitingDuration - (waitingDuration - 0.6f));
        MatchFound();
    }

    private void MatchFound()
    {
        characterAnim.TransitionToVSScreen();
    }

}
