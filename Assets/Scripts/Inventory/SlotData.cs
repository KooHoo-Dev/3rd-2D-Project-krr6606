using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SlotData : MonoBehaviour
{
    public RectTransform rectTransform { get; private set; }
    [field: SerializeField] public CatName catName { get; set; }
    [field: SerializeField] public GameLevelDataName levelName { get; set; }
    public int ResultSlotIndex;
    public Image image;
    public TMP_Text NameText;
    public TMP_Text LevelText;
    public TMP_Text upgradeEXP;

    public TMP_Text CostText;
    GameDataManager gameDataManager;

    public void Awake()
    {
        gameDataManager = GameDataManager.Instance;
        rectTransform = GetComponent<RectTransform>();
    }
    public void Start()
    {
        if (levelName != GameLevelDataName.none)
        {
            SetUserDataUI(levelName);
        }
    }
    public void SetNormalSlotData(CatName catName)
    {
        this.catName = catName;
        image.gameObject.SetActive(true);
        image.sprite = CatSlotSpriteDataManager.Instance.GetImageOrNull(catName);
        NameText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Name;
        int level = gameDataManager.CatSOArray[(int)catName].unitdata.Level;
        if (level == 10)
        {
            upgradeEXP.text = "Max";
        }
        else
        {

            LevelText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Level.ToString();
        }
        upgradeEXP.text = gameDataManager.CatSOArray[(int)catName].unitdata.UpgradeCost_1_10[gameDataManager.CatSOArray[(int)catName].unitdata.Level - 1].ToString();
    }
    public void SetResultSlotData(CatName catName)
    {
        this.catName = catName;
        image.gameObject.SetActive(true);
        image.sprite = CatSlotSpriteDataManager.Instance.GetCatSpriteOrNull(catName);

        if (CostText != null)
        {
            CostText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Cost.ToString();
        }
        if (catName == CatName.None)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void SetPosition(Vector2 inputPos)
    {
        rectTransform.anchoredPosition = inputPos;
    }
    public void UserDataLevelUp(GameLevelDataName levelName)
    {
        switch (levelName)
        {
            case GameLevelDataName.maxGoldLevel:
                if (gameDataManager.userGameDataSO.maxGoldLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                gameDataManager.userGameDataSO.maxGoldLevel++;
                }
                break;
            case GameLevelDataName.goldPerSecLevel:
                if (gameDataManager.userGameDataSO.goldPerSecLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                gameDataManager.userGameDataSO.goldPerSecLevel++;
                }
                break;
            case GameLevelDataName.skillDamageLevel:
                if (gameDataManager.userGameDataSO.skillDamageLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                gameDataManager.userGameDataSO.skillDamageLevel++;
                }
                break;
            case GameLevelDataName.skillCoolDownLevel:
                if (gameDataManager.userGameDataSO.skillCoolDownLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                gameDataManager.userGameDataSO.skillCoolDownLevel++;
                }
                break;
            case GameLevelDataName.skillRangeLevel:
                if (gameDataManager.userGameDataSO.skillRangeLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                gameDataManager.userGameDataSO.skillRangeLevel++;
                }
                break;
            case GameLevelDataName.EXPGetLevel:
                if (gameDataManager.userGameDataSO.EXPGetLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {

                    gameDataManager.userGameDataSO.EXPGetLevel++;
                }
                break;
            case GameLevelDataName.HeroSpeedLevel:
                if (gameDataManager.userGameDataSO.HeroSpeedLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {

                    gameDataManager.userGameDataSO.HeroSpeedLevel++;
                }
                break;
            case GameLevelDataName.HeroHealthLevel:
                if (gameDataManager.userGameDataSO.HeroHealthLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {

                    gameDataManager.userGameDataSO.HeroHealthLevel++;
                }
                break;
            case GameLevelDataName.HeroDamageLevel:
                if (gameDataManager.userGameDataSO.HeroDamageLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    gameDataManager.userGameDataSO.HeroDamageLevel++;
                }
                break;
            case GameLevelDataName.SpawnCoolDownLevel:
                if (gameDataManager.userGameDataSO.spawnCoolDownLevel < gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    gameDataManager.userGameDataSO.spawnCoolDownLevel++;
                }
                break;
            default: break;
        }
        SetUserDataUI(levelName);


    }
    public void SetUserDataUI(GameLevelDataName levelName)
    {
        switch (levelName)
        {
            case GameLevelDataName.maxGoldLevel:
                if (gameDataManager.userGameDataSO.maxGoldLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.maxGoldLevel.ToString();
                int index = gameDataManager.userGameDataSO.maxGoldLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index].ToString();
                break;
            case GameLevelDataName.goldPerSecLevel:
                if (gameDataManager.userGameDataSO.goldPerSecLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.goldPerSecLevel.ToString();
                int index1 = gameDataManager.userGameDataSO.goldPerSecLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index1].ToString();
                break;
            case GameLevelDataName.skillDamageLevel:
                if (gameDataManager.userGameDataSO.skillDamageLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.skillDamageLevel.ToString();
                int index2 = gameDataManager.userGameDataSO.skillDamageLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index2].ToString();
                break;
            case GameLevelDataName.skillCoolDownLevel:
                if (gameDataManager.userGameDataSO.skillCoolDownLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.skillCoolDownLevel.ToString();
                int index3 = gameDataManager.userGameDataSO.skillCoolDownLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index3].ToString();
                break;
            case GameLevelDataName.skillRangeLevel:
                if (gameDataManager.userGameDataSO.skillRangeLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.skillRangeLevel.ToString();
                int index4 = gameDataManager.userGameDataSO.skillRangeLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index4].ToString();
                break;
            case GameLevelDataName.EXPGetLevel:
                if (gameDataManager.userGameDataSO.EXPGetLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.EXPGetLevel.ToString();
                int index5 = gameDataManager.userGameDataSO.EXPGetLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index5].ToString();
                break;
            case GameLevelDataName.HeroSpeedLevel:
                if (gameDataManager.userGameDataSO.HeroSpeedLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.HeroSpeedLevel.ToString();
                int index6 = gameDataManager.userGameDataSO.HeroSpeedLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index6].ToString();
                break;
            case GameLevelDataName.HeroHealthLevel:
                if (gameDataManager.userGameDataSO.HeroHealthLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.HeroHealthLevel.ToString();
                int index7 = gameDataManager.userGameDataSO.HeroHealthLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index7].ToString();
                break;
            case GameLevelDataName.HeroDamageLevel:
                if (gameDataManager.userGameDataSO.HeroDamageLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.HeroDamageLevel.ToString();
                int index8 = gameDataManager.userGameDataSO.HeroDamageLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index8].ToString();
                break;
            case GameLevelDataName.SpawnCoolDownLevel:
                if (gameDataManager.userGameDataSO.spawnCoolDownLevel >= gameDataManager.userGameDataSO.LevelUpCost_1_10.Length)
                {
                    LevelText.text = "Max";
                    upgradeEXP.text = "";
                    return;
                }
                LevelText.text = gameDataManager.userGameDataSO.spawnCoolDownLevel.ToString();
                int index9 = gameDataManager.userGameDataSO.spawnCoolDownLevel - 1;
                upgradeEXP.text = gameDataManager.userGameDataSO.LevelUpCost_1_10[index9].ToString();
                break;
            default: break;
        }
    }
}