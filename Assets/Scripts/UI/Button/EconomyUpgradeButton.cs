using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EconomyUpgradeButton : MonoBehaviour
{
    Animator animator;
    public TMP_Text CostText;
    public TMP_Text LevelText;
    int Cost;
    InGameEconomyManager economyManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        economyManager = InGameEconomyManager.Instance;
        Cost = economyManager.DefaultEconomyUpgradeCost * economyManager.maxGoldLevel;
        CostText.text = $"{Cost}¿ø";
        LevelText.text = $"{economyManager.maxGoldLevel}";
    }
    private void Update()
    {
        animator.SetBool("CanUpgrade", economyManager.gold >= Cost);
    }
    public void UpgradeEconomy()
    {
        bool isMaxLevel = economyManager.maxGoldLevel == economyManager.maxLevel;
        if (economyManager.gold < Cost || isMaxLevel)
        {
            return;
        }

        economyManager.UpgradeEconomy();
        
        Cost = economyManager.DefaultEconomyUpgradeCost * economyManager.maxGoldLevel;
        CostText.text = economyManager.maxGoldLevel == economyManager.maxLevel ? $"MAX" : $"{Cost}¿ø";

        LevelText.text =  $"{economyManager.maxGoldLevel}";
    }
}
