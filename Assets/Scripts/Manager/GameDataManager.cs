using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    public UserGameDataSO userGameDataSO;
    
    
    // CatName과 CatSO를 연결하는 배열, CatName을 인덱스로 사용하여 CatSO를 가져올 수 있음
    public EntitySO[] CatSOArray; 

 
    public Dictionary<CatName, int> CatLevelDic;

    public int MaxCatSlotCount = 10;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            SetCatSlot(CatName.Cat1, 0);
            CatLevelDic = new Dictionary<CatName, int>();
            for (int i = 0; i < userGameDataSO.UnLockList.Count; i++)
            {
                CatLevelDic.Add(userGameDataSO.UnLockList[i], userGameDataSO.UnLockCatLevel[i]);
            }
            DontDestroyOnLoad(gameObject);
        }

    }

    public void CatUpgreade(CatName catName)
    {
        if (CatLevelDic.ContainsKey(catName))
        {
            CatLevelDic[catName]++;
        }
        else
        {
            Debug.LogWarning($"Cat {catName} is not unlocked yet.");
        }
    }
    public void UnlockCat(CatName catName)
    {
        if (!userGameDataSO.UnLockList.Contains(catName))
        {
            userGameDataSO.UnLockList.Add(catName);
            userGameDataSO.UnLockCatLevel.Add(1);
            CatLevelDic.Add(catName, 1);
        }
    }
    public void SetCatSlot(CatName catName, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < MaxCatSlotCount)
        {
            userGameDataSO.CatSlotArray[slotIndex] = catName;
        }
    }
}