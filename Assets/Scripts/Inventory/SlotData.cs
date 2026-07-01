using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotData : MonoBehaviour
{
    private RectTransform rectTransform;
   [field: SerializeField] public CatName catName { get; set; }
    public int slotIndex;
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

    public void SetNormalSlotData(CatName catName)
    {
        this.catName = catName;
        image.sprite = CatSlotSpriteDataManager.Instance.GetImageOrNull(catName);
        NameText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Name;
        int level = gameDataManager.CatSOArray[(int)catName].unitdata.Level;
        if(level == 10)
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
        CostText.text = gameDataManager.CatSOArray[(int)catName].unitdata.Cost.ToString();
        if(catName == CatName.None)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void SetPosition(Vector2 inputPos)
    {
        rectTransform.anchoredPosition = inputPos;
    }
}
