using System.Collections;
using UnityEngine;

public class Tutorial00 : MonoBehaviour
{
    public GameObject tutorialPanel01;
    public GameObject tutorialPanel02;
    public GameObject tutorialArrow;
    bool tutorialSucces = false;
    void Start()
    {
        StartCoroutine(WaitOnTutorial());
    }
    private void Update()
    {
        if (tutorialSucces == false && InGameEconomyManager.Instance.gold > 50)
        {
            tutorialArrow.SetActive(true);
            tutorialSucces = true;
        }
        
    }
    public void OnNextPanel()
    {
        tutorialPanel01.SetActive(false);
        tutorialPanel02.SetActive(true);
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
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

        TimeManager.Instance.PauseGame();
    }
    public void OffTutorial()
    {
        tutorialPanel01.SetActive(false);
        tutorialPanel02.SetActive(false);   
        TimeManager.Instance.ResumeGame();
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
    }
}
