using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    Button button;
    EXPUI expUI;
    Swipe_Slot swipeSlot;
    void Awake()
    {
        button = GetComponent<Button>();
        expUI = FindAnyObjectByType<EXPUI>();
        swipeSlot = FindAnyObjectByType<Swipe_Slot>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnPowerUpButtonClicked);
    }

    public void OnPowerUpButtonClicked()
    {
        GameDataManager gameDataManager = GameDataManager.Instance;
        int currentEXP = gameDataManager.userGameDataSO.currentEXP;
        int index = swipeSlot.currentIndex;
        CatName catName = swipeSlot.slotDataArray[index].catName;
        GameLevelDataName gameLevelDataName = swipeSlot.slotDataArray[index].levelName;
        if (catName == CatName.None && gameLevelDataName == GameLevelDataName.none)
        {
            return;
        }
        if(catName == CatName.None && gameLevelDataName != GameLevelDataName.none)
        {
            int level = gameDataManager.GetLevelData(gameLevelDataName);
            int EXP = gameDataManager.userGameDataSO.LevelUpCost_1_10[level - 1];
            if (currentEXP < EXP) return;
            currentEXP -= EXP;
            gameDataManager.userGameDataSO.currentEXP = currentEXP;
            UserDataLevelUp(swipeSlot.slotDataArray[index]);
            expUI.UpdateEXPUI();
            return;
        }
        int currentLevel = gameDataManager.CatLevelDic[catName];
        int requiredEXP = gameDataManager.CatSOArray[(int)catName].unitdata.UpgradeCost_1_10[gameDataManager.CatSOArray[(int)catName].unitdata.Level];
        if (currentEXP < requiredEXP) return;
        currentEXP -= requiredEXP;
        gameDataManager.userGameDataSO.currentEXP = currentEXP;
        gameDataManager.CatSOArray[(int)catName].unitdata.Level++;
        swipeSlot.slotDataArray[index].SetNormalSlotData(catName);
        expUI.UpdateEXPUI();
    }
    public void UserDataLevelUp(SlotData slotData)
    {
        slotData.UserDataLevelUp(slotData.levelName);
    }

}
