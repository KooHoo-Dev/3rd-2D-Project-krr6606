using UnityEngine;
using System.Collections;
public class Tutorial02 : MonoBehaviour
{
    public GameObject tutorialPanel01; 
    public GameObject tutorialPanel02;
    public GameObject tutorialArrow;
    public HeroSkill01 skill;
    bool tutorialSucces = false;
    private void Awake()
    {
        skill = FindAnyObjectByType<HeroSkill01>();
    }
    void Start()
    {
        StartCoroutine(WaitOnTutorial());
    }
    private void Update()
    {
        if (tutorialSucces == false && skill.currentSkillCooldown <= 0)
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

        TimeManager.Instance.PauseGame();
    }
    public void OnNextTutorial()
    {
        tutorialPanel01.SetActive(false);
        tutorialPanel02.SetActive(true);
    }
    public void OffTutorial()
    {
        tutorialPanel01.SetActive(false);
        tutorialPanel02.SetActive(false);
        TimeManager.Instance.ResumeGame();
    }
}
