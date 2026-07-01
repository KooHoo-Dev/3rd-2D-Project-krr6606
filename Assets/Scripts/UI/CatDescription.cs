using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class CatDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    Swipe_Slot swipe_Slot;
    int currentIndex;
    public void Start()
    {
        swipe_Slot = FindAnyObjectByType<Swipe_Slot>();
    }
    public void Update()
    {
        if (swipe_Slot.currentIndex != currentIndex)
        {
            currentIndex = swipe_Slot.currentIndex;
            SetDescription(GameDataManager.Instance.CatSOArray[(int)swipe_Slot.slotDataArray[currentIndex].catName].unitdata.Description);
        }
    }
    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}
