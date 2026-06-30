using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotData : MonoBehaviour
{
    public CatName catName;
    public Image image;
    public TMP_Text NameText;
    public TMP_Text LevelText;
    public TMP_Text upgradeEXP;
    GameDataManager gameDataManager;

    public void Awake()
    {
        gameDataManager = GameDataManager.Instance;
    }
    public void SetSlotData(CatName catName)
    {
        this.catName = catName;
        image.sprite = CatSlotSpriteDataManager.Instance.GetImageOrNull(catName);
        NameText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Name;
        LevelText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Level.ToString();
        upgradeEXP.text = gameDataManager.CatSOArray[(int)catName].unitdata.UpgradeCost_1_10[gameDataManager.CatSOArray[(int)catName].unitdata.Level - 1].ToString();
    }
    public CatName GetCatName()
    {
        return catName;
    }
}
