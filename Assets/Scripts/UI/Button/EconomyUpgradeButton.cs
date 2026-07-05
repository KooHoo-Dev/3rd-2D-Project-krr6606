using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EconomyUpgradeButton : MonoBehaviour
{
    Animator animator;
    Image buttonImage;
    public TMP_Text CostText;
    public TMP_Text LevelText;
    int Cost;
    InGameEconomyManager economyManager;
    public Color LockColor = new Color(0.55f, 0.55f, 0.55f, 1f);
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        buttonImage = animator.GetComponent<Image>();
        economyManager = InGameEconomyManager.Instance;
        Cost = economyManager.DefaultEconomyUpgradeCost;
        CostText.text = $"{Cost}원";
        LevelText.text = $"{economyManager.maxGoldLevel}";
    }
    private void Update()
    {
        animator.SetBool("CanUpgrade", economyManager.gold >= Cost);
        if(economyManager.gold < Cost)
        {
            buttonImage.color = LockColor;
        }
        else
        {
            buttonImage.color = Color.white;
        }
    }
    public void UpgradeEconomy()
    {
        bool isMaxLevel = economyManager.maxGoldLevel == economyManager.maxLevel;
        if (economyManager.gold < Cost || isMaxLevel)
        {
            StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
            return;
        }
        StringSoundManager.Instance.PlaySFX("Snd-머니 사용");
        economyManager.UpgradeEconomy();

        economyManager.SpendGold(Cost);
        Cost = economyManager.DefaultEconomyUpgradeCost + (economyManager.maxGoldLevel - 1) * economyManager.DefaultEconomyUpgradeCost;
        CostText.text = economyManager.maxGoldLevel == economyManager.maxLevel ? $"MAX" : $"{Cost}원";

        LevelText.text =  $"{economyManager.maxGoldLevel}";
    }
}
