using UnityEngine;
using TMPro;
public class InGameEconomyManager : MonoBehaviour
{
    public static InGameEconomyManager Instance { get; private set; }
    public int gold { get; private set; } = 0;
    private float goldBuffer = 0;
    public int MaxGold { get; private set; } = 1000;
    public float goldPerSec { get; private set; } = 2f;
    public int goldPerSecLevel { get; private set; } = 1;
    public int maxGoldLevel { get; private set; } = 1;
    public int maxLevel { get; private set; } = 10;
    public int DefaultIncreaseAmountMaxGold { get; private set; } = 50;
    public float DefaultIncreaseAmountGoldPerSec { get; private set; } = 1f;

    public int DefaultEconomyUpgradeCost { get; private set; } = 40;
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

    private void Update()
    {
        IncreaseGold(goldPerSec * Time.deltaTime + ((goldPerSecLevel  - 1) * DefaultIncreaseAmountGoldPerSec * Time.deltaTime * GameDataManager.Instance.userGameDataSO.goldPerSecLevel));
        UpdateGoldUI();
    }
    public void UpgradeEconomy()
    {
        goldPerSecLevel++;
        maxGoldLevel++;
        MaxGold += DefaultIncreaseAmountMaxGold 
            + (GameDataManager.Instance.userGameDataSO.goldPerSecLevel - 1)* DefaultIncreaseAmountMaxGold
            + (int)((maxGoldLevel - 1) * DefaultIncreaseAmountMaxGold * 0.5f * GameDataManager.Instance.userGameDataSO.maxGoldLevel);
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
