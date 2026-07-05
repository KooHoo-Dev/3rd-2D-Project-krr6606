using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;


    public void PauseGame()
    {
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
        TimeManager.Instance.PauseGame();
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        StringSoundManager.Instance.PlaySFX("Snd-클릭");
        TimeManager.Instance.ResumeGame();
        pausePanel.SetActive(false);

    }
}
