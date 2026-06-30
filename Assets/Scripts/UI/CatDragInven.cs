using System.Collections.Generic;
using UnityEngine;

public class CatDragInven : MonoBehaviour
{
    List<SlotData> slotDatas;
    public GameObject slotPrefab;
    public InvenType invenType;
    Swipe_Slot swipe_Slot;
    GameDataManager gameDataManager;

    private void Awake()
    {
        swipe_Slot = GetComponent<Swipe_Slot>();
    }
    private void Start()
    {

        gameDataManager = GameDataManager.Instance;

        Init();
    }

    public CatName GetCatName(int index)
    {
        if (index >= 0 && index < slotDatas.Count)
        {
            return slotDatas[index].catName;
        }
        return CatName.None;
    }
    public void SetCatName(int index, CatName catName)
    {
        if (index >= 0 && index < slotDatas.Count)
        {
            slotDatas[index].catName = catName;
        }
    }
    private void Init()
    {
        int count = gameDataManager.userGameDataSO.UnLockList.Count;
        for (int i = 0; i < count; i++)
        {
            if(isInList(gameDataManager.userGameDataSO.UnLockList[i]))
            {
                continue;
            }
            GameObject slotObj = Instantiate(slotPrefab, transform);
            SlotData slotData = slotObj.GetComponent<SlotData>();
            CatName catName = gameDataManager.userGameDataSO.UnLockList[i];
            slotData.SetSlotData(catName);
            slotDatas.Add(slotData);
        }
    }
    private bool isInList(CatName catName)
    {
        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i].catName == catName)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsIn(SlotData slot)
    {
        if (slot == null) return false;

        for (int i = 0; i < slotDatas.Count; i++)
        {
            if (slotDatas[i] == slot)
            {
                return true;
            }
        }


        return false;
    }
}
