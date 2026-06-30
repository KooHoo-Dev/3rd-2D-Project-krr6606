using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public CatSpawner catSpawner;
    public static SpawnManager Instance { get; private set; }
    public CatSpawnSlotParent spawnSlotParent;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        CatName[] catSlotArray = GameDataManager.Instance.userGameDataSO.CatSlotArray;
        for(int i = 0; i < catSlotArray.Length; i++)
        {
            Debug.Log($"스폰 슬롯 초기화 {catSlotArray[i]}, {i}번째");
        }
        for (int i = 0; i < catSlotArray.Length; i++)
        {
            CatName catName = catSlotArray[i];
            if (catName != CatName.None)
            {
                Debug.Log($"스폰 슬롯 초기화 {catName}");
                float Interval = GameDataManager.Instance.CatSOArray[(int)catName].unitdata.InitColdDown;
                int cost = GameDataManager.Instance.CatSOArray[(int)catName].unitdata.Cost;
                spawnSlotParent.SetSpawnSlot(catName, Interval, cost, i);
            }
        }
    }

}
