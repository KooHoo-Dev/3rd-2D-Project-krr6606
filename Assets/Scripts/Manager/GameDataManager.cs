using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    public int goldPerSecLevel = 1;
    public int maxGoldLevel = 1;
    // CatName과 CatSO를 연결하는 배열, CatName을 인덱스로 사용하여 CatSO를 가져올 수 있음
    public EntitySO[] CatSOArray; 

    public List<CatName> UnLockList;
    public CatName[] CatSlotArraty;
    public int MaxCatSlotCount = 10;
    public int CatSlotCount = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            CatSlotArraty = new CatName[MaxCatSlotCount];
            for (int i = 0; i < MaxCatSlotCount; i++)
            {
                CatSlotArraty[i] = CatName.None;

            }
            SetCatSlot(CatName.Cat1, 0);
            SetCatSlot(CatName.Cat2, 1);
            SetCatSlot(CatName.Cat3, 2);
            DontDestroyOnLoad(gameObject);
        }
    }


    public void UnlockCat(CatName catName)
    {
        if (!UnLockList.Contains(catName))
        {
            UnLockList.Add(catName);
        }
    }
    public void SetCatSlot(CatName catName, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < MaxCatSlotCount)
        {
            CatSlotArraty[slotIndex] = catName;
        }
    }
}