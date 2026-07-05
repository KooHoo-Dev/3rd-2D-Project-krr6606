using UnityEngine;
using System.Collections;
public class Tutorial01 : MonoBehaviour
{
    public GameObject tutorialPanel01; 
    public GameObject tutorialArrow;
    bool tutorialSucces = false;
    void Start()
    {
        StartCoroutine(WaitOnTutorial());
    }
    private void Update()
    {
        if (tutorialSucces == false && InGameEconomyManager.Instance.gold > 40)
        {
            tutorialArrow.SetActive(true);
            tutorialSucces = true;
        }

    }
    public void OfftutorialArrow()
    {
        tutorialArrow.SetActive(false);
    }
    private IEnumerator WaitOnTutorial()
    {
        yield return new WaitForSeconds(2f);
        OnTutorial();
    }

    public void OnTutorial()
    {
        tutorialPanel01.SetActive(true);
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
        TimeManager.Instance.PauseGame();
    }
    public void OffTutorial()
    {
        tutorialPanel01.SetActive(false);
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
        TimeManager.Instance.ResumeGame();
    }
}
