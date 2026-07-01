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
        if(catName == CatName.None)
        {
            Debug.Log("No cat selected for power-up.");
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

}
