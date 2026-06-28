using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{

    public float timeScale = 1f;
    public static bool isPaused;
    public static bool isSpeedUp;
    public float speedUpTimeScale = 2f;
    public GameObject pausePanel;
    public Image speedUpButtonImage;

    private void Update()
    {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;
    }
    public void PauseGame()
    {
        timeScale = 0f;
                isPaused = true;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        timeScale = isSpeedUp ? speedUpTimeScale : 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }
    public void ToggleSpeedUpGame()
    {
        if (isSpeedUp)
        {
            NormalSpeedGame();
        }
        else
        {
            SpeedUpGame();
        }
    }
    public void SpeedUpGame()
    {
        isSpeedUp = true;
        timeScale = speedUpTimeScale;
        speedUpButtonImage.color = Color.white;
    }
    public void NormalSpeedGame()
    {
        isSpeedUp = false;
        timeScale = 1f;
        speedUpButtonImage.color = new Color(0.5f, 0.5f, 0.5f);
    }
}
