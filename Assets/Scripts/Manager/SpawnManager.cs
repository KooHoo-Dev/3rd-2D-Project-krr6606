using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    
    public CatSpawner catSpawner;
    public CatSpawnSlot[] catSpawnSlots;
    public static SpawnManager Instance { get; private set; }
    CatSpawnSlotParent spawnSlotParent;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            spawnSlotParent = FindAnyObjectByType<CatSpawnSlotParent>();
            Instance = this;
        }
    }
    void Start()
    {
        SlotsInit();
    }
    void Update()
    {
        if(StageManager.Instance.isStageEnd) return;
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            catSpawnSlots[0].Spawn();
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            catSpawnSlots[1].Spawn();
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            catSpawnSlots[2].Spawn();
        }
        if(Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            catSpawnSlots[3].Spawn();
        }
        if(Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            catSpawnSlots[4].Spawn();
        }
    }
    void SlotsInit()
    {
        CatName[] catSlotArray = GameDataManager.Instance.userGameDataSO.CatSlotArray;

        for (int i = 0; i < catSlotArray.Length; i++)
        {
            CatName catName = catSlotArray[i];
            if (catName != CatName.None)
            {
                Debug.Log($"스폰 슬롯 초기화 {catName}");
                float Interval = GameDataManager.Instance.CatSOArray[(int)catName].unitdata.InitColdDown;
                Interval -= GameDataManager.Instance.userGameDataSO.levelDataList[(int)GameLevelDataName.skillCoolDownLevel].level * 0.2f;
                if (Interval < 2)
                {
                    Interval = 2;
                }
                int cost = GameDataManager.Instance.CatSOArray[(int)catName].unitdata.Cost;
                spawnSlotParent.SetSpawnSlot(catName, Interval, cost, i);
            }
        }
        catSpawnSlots = spawnSlotParent.GetComponentsInChildren<CatSpawnSlot>();
    }
}
