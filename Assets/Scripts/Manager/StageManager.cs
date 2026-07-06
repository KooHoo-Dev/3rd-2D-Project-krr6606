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

    EnemySpawner enemySpawner;
    public CatSpawner catSpawner;
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
        }
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        catSpawner = FindAnyObjectByType<CatSpawner>();
        ResultPanel.SetActive(false);

    }
    private void Start()
    {
        currentStage = StageDataManager.Instance.GetStageData(CurrentStageName);
    }
    public void GameWin()
    {
        if(isStageEnd) return;
        ResultPanel.SetActive(true);
        ResultText.text = "완전 승리!";
        EXPBar.SetActive(true);
        EXPText.text = $"{currentStage.DefaultEXPReward}";
        enemySpawner.isSpawnStopped = true;
        isStageEnd = true;
        AllEnemyDie();
        StringSoundManager.Instance.PlaySFX("Snd-승리");
    }
    private void AllEnemyDie()
    {
        for (int i = 0; i < enemySpawner.enemyPoolList.Count; i++)
        {
            Unit[] Enemys = enemySpawner.GetAllEnemies();
            for(int j = 0; j < Enemys.Length; j++)
            {
                Enemys[j].Kill();
            }
        }
    }
    private void AllCatDie()
    {
        for (int i = 0; i < catSpawner.catPoolList.Count; i++)
        {
            Unit[] Cats = catSpawner.GetAllCats();
            for (int j = 0; j < Cats.Length; j++)
            {
                Cats[j].Kill();
            }
        }
    }
    public void GameLose()
    {
        if(isStageEnd) return;
        ResultPanel.SetActive(true);
        ResultText.text = "패배...";
        EXPBar.SetActive(false);
        enemySpawner.isSpawnStopped = true;
        isStageEnd = true;
        AllCatDie();
        hero.Kill();
        StringSoundManager.Instance.PlaySFX("Snd-패배");
    }
    public bool IsStageEnd()
    {
        return isStageEnd;
    }
}
