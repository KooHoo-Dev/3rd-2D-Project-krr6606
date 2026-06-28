using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    
    public Hero hero;

    public Tower EnemyTower;
    public GameObject ResultPanel;
    public TMP_Text ResultText;
    public GameObject EXPBar;
    public TMP_Text EXPText;
    public StageName CurrentStageName;
    private _Stage currentStage;

    public bool isStageEnd = false;
    void Awake()
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
        ResultPanel.SetActive(false);

    }
    private void Start()
    {
        currentStage = StageDataManager.Instance.GetStageData(CurrentStageName);
    }
    public void GameWin()
    {
        ResultPanel.SetActive(true);
        ResultText.text = "완전 승리!";
        EXPBar.SetActive(true);
        EXPText.text = $"{currentStage.DefaultEXPReward}";
    }
    public void GameLose()
    {
        ResultPanel.SetActive(true);
        ResultText.text = "패배...";
        EXPBar.SetActive(false);
    }
    public bool IsStageEnd()
    {
        return isStageEnd;
    }
}
