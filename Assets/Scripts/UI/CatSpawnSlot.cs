using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CatSpawnSlot : MonoBehaviour
{
    private CatSpawner catSpawner;
    public CatName CatName { get; set; } = CatName.None;
    public Image CatImage;
    public TMP_Text CostText;
    public float SpawnInterval { get; private set; } = 2f;
    public float SpawnTime { get; private set; } = 0;
    public int Cost { get; private set; } = 50;
    private Color LockColor = new Color(0.55f, 0.55f, 0.55f, 1f);

    private void Awake()
    {
      
        catSpawner = FindAnyObjectByType<CatSpawner>();
    }
    public void Update()
    {
        ReduceSpawnTime(Time.deltaTime);
        if(SpawnTime <= 0)
        {
            CatImage.color = Color.white;
        }
    }

    public void Spawn()
    {
        if (IsReadyToSpawn())
        {
            catSpawner.Spawn(CatName);
            InGameEconomyManager.Instance.SpendGold(Cost);
            SpawnTime = SpawnInterval;
            CatImage.color = LockColor;
        }
    }
    public void ReduceSpawnTime(float amount)
    {
        SpawnTime -= amount;
        if (SpawnTime < 0)
        {
            SpawnTime = 0;
        }
    }
    public bool IsReadyToSpawn()
    {
        return SpawnTime <= 0 && Cost <= InGameEconomyManager.Instance.gold;
    }
    public void SetSpawnInterval(float interval)
    {
        SpawnInterval = interval;
        SpawnTime = 0;
    }
    
    public void InitCatSlot(CatName catName, float spawnInterval ,int cost)
    {
        SetSpawnInterval(spawnInterval);
        CatName = catName;
        Cost = cost;
        Sprite catSprite = CatSlotSpriteDataManager.Instance.GetCatSpriteOrNull(CatName);
        CatImage.gameObject.SetActive(true);
        CostText.gameObject.SetActive(true);
        CostText.text = Cost.ToString();
        if (catSprite != null)
        {
            CatImage.sprite = catSprite;
        }
        Debug.Log($"Initialized CatSpawnSlot for {catName} with spawn interval {spawnInterval} and cost {cost}");
    }


}
