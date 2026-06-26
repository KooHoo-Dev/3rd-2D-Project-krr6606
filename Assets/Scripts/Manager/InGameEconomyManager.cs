using UnityEngine;
using TMPro;
public class InGameEconomyManager : MonoBehaviour
{
    public static InGameEconomyManager Instance { get; private set; }
    private int gold = 0;
    private float goldBuffer = 0;
    private int MaxGold = 100;
    private float goldPerSec = 2f;
    private int goldPerSecLevel = 1;
    private int maxGoldLevel = 1;
    public TMP_Text goldText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        goldPerSecLevel = GameDataManager.Instance.goldPerSecLevel;
        maxGoldLevel = GameDataManager.Instance.maxGoldLevel;
    }
    private void Update()
    {
        IncreaseGold(goldPerSec * Time.deltaTime + (goldPerSecLevel * 0.5f * goldPerSec));
    }
    public void IncreaseGold(float amount)
    {
        
        goldBuffer += amount;
        if(goldBuffer > 1)
        {
            int goldToAdd = Mathf.FloorToInt(goldBuffer);
            goldBuffer -= goldToAdd;
            gold += goldToAdd;
        }

        if (gold > MaxGold)
        {
            gold = MaxGold;
        }
        UpdateGoldUI();
    }
    private void UpdateGoldUI()
    {
        goldText.text = $"{gold} / {MaxGold}"; 
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough gold!");
            return false;
        }
    }
    public void AddGold(int amount)
    {
        gold += amount;
        if (gold > MaxGold)
        {
            gold = MaxGold;
        }
        UpdateGoldUI();
    }
}
