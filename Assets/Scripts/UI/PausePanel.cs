using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;


    public void PauseGame()
    {
        TimeManager.Instance.PauseGame();
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        TimeManager.Instance.ResumeGame();
        pausePanel.SetActive(false);

    }
}
