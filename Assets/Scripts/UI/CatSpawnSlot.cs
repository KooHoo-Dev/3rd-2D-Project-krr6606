using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CatSpawnSlot : MonoBehaviour
{
    private CatSpawner catSpawner;
    public CatName CatName { get; set; } = CatName.None;
    public Image CatImage;
    public TMP_Text CostText;
    public GameObject ProgressBarBackgorund;
    public Image ProgressBar;
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
        if (CatImage.gameObject.activeSelf == false) return;
        if (IsReadyToSpawn())
        {
            CatImage.color = Color.white;
        }
        else
        {
            CatImage.color = LockColor;
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
            ProgressBarBackgorund.SetActive(true);
            ProgressBar.gameObject.SetActive(true);

            CostText.gameObject.SetActive(false);
            ProgressBar.fillAmount = 0;
            StringSoundManager.Instance.PlaySFX("Snd-머니 사용");
            StartCoroutine(SpawnIntervalCo());
        }
        else
        {
            StringSoundManager.Instance.PlaySFX("Snd-사용 불가");
        }
    }
    public IEnumerator SpawnIntervalCo()
    {
        while (SpawnTime > 0)
        {
            ReduceSpawnTime(Time.deltaTime);
            ProgressBar.fillAmount = 1 - (SpawnTime / SpawnInterval);
            yield return null;
        }
        ProgressBarBackgorund.SetActive(false);
        CostText.gameObject.SetActive(true);
        CatImage.color = Color.white;
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

    public void InitCatSlot(CatName catName, float spawnInterval, int cost)
    {
        SetSpawnInterval(spawnInterval);
        CatName = catName;
        Cost = cost;
        Sprite catSprite = CatSlotSpriteDataManager.Instance.GetCatSpriteOrNull(CatName);
        CatImage.gameObject.SetActive(true);
        CostText.gameObject.SetActive(true);
        CostText.text = Cost.ToString();
        ProgressBarBackgorund.SetActive(false);
        CatImage.color = Color.white;

        if (catSprite != null)
        {
            CatImage.sprite = catSprite;
        }
        Debug.Log($"Initialized CatSpawnSlot for {catName} with spawn interval {spawnInterval} and cost {cost}");
    }


}
