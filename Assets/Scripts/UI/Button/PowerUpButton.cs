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


    public void OnPowerUpButtonClicked()
    {
        GameDataManager gameDataManager = GameDataManager.Instance;
        int currentEXP = gameDataManager.userGameDataSO.currentEXP;
        int index = swipeSlot.currentIndex;
        CatName catName = swipeSlot.slotDataList[index].catName;
        GameLevelDataName gameLevelDataName = swipeSlot.slotDataList[index].levelName;
        if (catName == CatName.None && gameLevelDataName == GameLevelDataName.none)
        {
            StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
            return;
        }
        if(catName == CatName.None && gameLevelDataName != GameLevelDataName.none)
        {
            int level = gameDataManager.GetLevelData(gameLevelDataName);
            int EXP = gameDataManager.userGameDataSO.LevelUpCost_1_10[level - 1];
            if (currentEXP < EXP) 
            {
                StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
                return;
            } 
            currentEXP -= EXP;
            gameDataManager.userGameDataSO.currentEXP = currentEXP;
            UserDataLevelUp(swipeSlot.slotDataList[index]);
            expUI.UpdateEXPUI();
            StringSoundManager.Instance.PlaySFX("Snd-경험치 사용");
            return;
        }
        int currentLevel = gameDataManager.CatLevelDic[catName];
        int requiredEXP = gameDataManager.CatSOArray[(int)catName].unitdata.UpgradeCost_1_10[gameDataManager.CatSOArray[(int)catName].unitdata.Level];
        if (currentEXP < requiredEXP) 
        { 
            StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
            return;
        }
        currentEXP -= requiredEXP;
        gameDataManager.userGameDataSO.currentEXP = currentEXP;
        gameDataManager.CatSOArray[(int)catName].unitdata.Level++;
        swipeSlot.slotDataList[index].SetNormalSlotData(catName);
        expUI.UpdateEXPUI();
        StringSoundManager.Instance.PlaySFX("Snd-경험치 사용");
    }
    public void UserDataLevelUp(SlotData slotData)
    {
        slotData.UserDataLevelUp(slotData.levelName);
    }

}
