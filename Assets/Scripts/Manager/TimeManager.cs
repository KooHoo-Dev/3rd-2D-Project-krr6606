using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public float timeScale = 1f;
    public  bool isPaused;
    public  bool isSpeedUp;
    public float speedUpTimeScale = 2f;


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;
    }
    public void PauseGame()
    {
        timeScale = 0f;
                isPaused = true;

    }
    public void ResumeGame()
    {
        timeScale = isSpeedUp ? speedUpTimeScale : 1f;
        isPaused = false;

    }

    public void SpeedUpGame()
    {
        isSpeedUp = true;
        timeScale = speedUpTimeScale;

    }
    public void NormalSpeedGame()
    {
        isSpeedUp = false;
        timeScale = 1f;

    }
}
