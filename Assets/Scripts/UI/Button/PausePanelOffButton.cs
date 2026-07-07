using UnityEngine;
using UnityEngine.UI;

public class PausePanelOffButton : MonoBehaviour
{
     GameObject PausePanel;
     Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        PausePanel = GameObject.Find("PausePanel");
    }
    private void Start()
    {
        button.onClick.AddListener(TurnOffPausePanel);
    }
    public void TurnOffPausePanel()
    {
        TimeManager.Instance.ResumeGame();
        PausePanel.SetActive(false);
    }
}
