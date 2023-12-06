using System.Collections;
using UnityEngine;

public class AnimationsControl : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private GameObject vsScreen;
    [SerializeField] private GameObject waitingText;
    [SerializeField] private GameObject playButton;

    [Header("Animation Managers")]
    [SerializeField] private FadeTransitionManager detailsFading;
    [SerializeField] private CharacterAnim characterAnim;

    [Header("Match Settings")]
    [SerializeField] private float waitingDuration;
    [SerializeField] private float transitionDelay;

    private Coroutine matchFoundCoroutine;

    private void Start()
    {
        vsScreen.SetActive(false);
        StartMatchFindingSequence();
    }

    public void StartFindingMatch()
    {
        waitingText.SetActive(true);
        detailsFading.ActivatePlayButton(false, 0f);
        detailsFading.AppearAndRise();
        StartMatchFindingSequence();
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

    private void StartMatchFindingSequence() => matchFoundCoroutine = StartCoroutine(DelayedMatchFound());

    private IEnumerator DelayedMatchFound()
    {
        yield return new WaitForSeconds(waitingDuration);
        detailsFading.FadeAndDisappear();
        yield return new WaitForSeconds(transitionDelay);
        MatchFound();
    }

    private void MatchFound() => characterAnim.TransitionToVSScreen();
}
