using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform PoolParnt;
    private GameObject[] isSpawned; // �� y�� ��ġ�� ���� �̹� �����Ǿ����� ���θ� ��Ÿ���� �迭
    [field: SerializeField] private List<Unit> enemyList = new List<Unit>();

    public List<ComponentPool> enemyPoolList = new List<ComponentPool>();
    _Stage stage;
    List<StageWaveData> waveList;
    private float SpawnTime;
    StageManager stageManager;
    private float CurrentSpawnTime;

    public int MaxSpawnCount = 50;
    private int currentWaveIndex;
    public bool isSpawnStopped = false;
    public float SpawnFrameDelay = 0.2f;

    private WaitForSeconds WaitSpawnFrameDelay;
    bool isBossSpawned = false;
    int BossTriggerHealthPercentage;
    private void Awake()
    {
        WaitSpawnFrameDelay = new WaitForSeconds(SpawnFrameDelay);
    }
    private void Start()
    {
        isSpawned = new GameObject[MaxSpawnCount];
        StageName currentStageName = StageManager.Instance.CurrentStageName;

        stage = StageDataManager.Instance.GetStageData(currentStageName);

        waveList = stage.waves;

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyPoolList.Add(new ComponentPool(enemyList[i].gameObject, PoolParnt));
        }
        stageManager = StageManager.Instance;
        BossTriggerHealthPercentage = StageDataManager.Instance.GetStageData(stageManager.CurrentStageName).BossTriggerHealthPercentage;
        StartCoroutine(WaveCo());
    }

    public Unit[] GetAllEnemies()
    {
        List<Unit> allEnemies = new List<Unit>();
        foreach (var pool in enemyPoolList)
        {
            allEnemies.AddRange(pool.GetAllActiveUnit());
        }
        return allEnemies.ToArray();
    }
    private IEnumerator WaveCo()
    {
        SpawnTime = waveList[currentWaveIndex].IsRandomTime ?
            Random.Range(waveList[currentWaveIndex].MinSpawnInterval, waveList[currentWaveIndex].MaxSpawnInterval)
            : waveList[currentWaveIndex].MinSpawnInterval;
        CurrentSpawnTime = SpawnTime;
        while (true)
        {
            if (stageManager.IsStageEnd())
            {
                yield break;
            }
            int activeCount = 0;
            for (int i = 0; i < isSpawned.Length; i++)
            {
                if (isSpawned[i] == null)
                {
                    continue;
                }
                if (isSpawned[i].activeSelf)
                {
                    activeCount++;
                }
            }
            isSpawnStopped = activeCount >= MaxSpawnCount;
            Debug.Log($"���� Ȱ��ȭ�� �� ��: {activeCount}, �ִ� ���� ��: {MaxSpawnCount}, ���� ���� ����: {isSpawnStopped}");
            // ���� ���� ���� Ȯ��
            if (BossTriggerHealthPercentage > 0 && isBossSpawned == false)
            {
                int currentHealthPercentage = Mathf.RoundToInt((float)stageManager.EnemyTower.CurrentHealth / stageManager.EnemyTower.MaxHealth * 100);
                if (currentHealthPercentage <= BossTriggerHealthPercentage) // ���� ü�� ������ ���� ���� ���� ������ ���
                {
                    isBossSpawned = true;
                    EnemyName bossName = stage.BossName;
                    if (bossName == EnemyName.None)
                    {
                        Debug.LogError($"�������� {stage.name}�� BossName�� None���� �����Ǿ� �ֽ��ϴ�. �ùٸ� ���� �������ּ���.");
                    }
                    else
                    {
                        Unit[] Cats = StageManager.Instance.catSpawner.GetAllCats();
                        for (int i = 0; i < Cats.Length; i++)
                        {
                            Cats[i].HitBack();
                        }
                        Hero hero = StageManager.Instance.hero;

                        hero.HitBack();
                        Unit boss = Spawn(bossName); // ���� ����
                        boss.useData.DefaultHealth *= 2; // ���� ü�� 2�� ����
                        boss.CurrentHealth = boss.useData.DefaultHealth; // ���� ü�µ� �ִ� ü������ ����
                        boss.useData.DefaultDamage *= 1.1f; // ���� ���ݷ� 1.1�� ����
                        StringSoundManager.Instance.PlaySFX("Snd-���� ����");
                        StringSoundManager.Instance.PlayBGM("Bgm-����-5-����");
                    }
                }
            }
            // ���̺� ������ �����Ǿ����� Ȯ��
            if (isSpawnStopped)
            {
                yield return null;
                continue;
            }
            if (CurrentSpawnTime > 0)
            {
                CurrentSpawnTime -= Time.deltaTime;

            }
            else 
            {
                Debug.Log($"���̺� {currentWaveIndex} ���� ����: {waveList[currentWaveIndex].EnemyName}, ���� ��: {waveList[currentWaveIndex].SpawnCount}");

                for (int i = 0; i < waveList[currentWaveIndex].SpawnCount; i++)
                {
                    Spawn(waveList[currentWaveIndex].EnemyName);
                    Debug.Log($"���̺� {currentWaveIndex} ���� ��: {waveList[currentWaveIndex].EnemyName}, ���� ��: {i + 1}/{waveList[currentWaveIndex].SpawnCount}");
                    yield return WaitSpawnFrameDelay;
                }
                currentWaveIndex = waveList[currentWaveIndex].NextWaveIndex;
                SpawnTime = waveList[currentWaveIndex].IsRandomTime
                            ? Random.Range(waveList[currentWaveIndex].MinSpawnInterval, waveList[currentWaveIndex].MaxSpawnInterval)
                            : waveList[currentWaveIndex].MinSpawnInterval;
                CurrentSpawnTime = SpawnTime;
                if (currentWaveIndex < 0)
                {
                    Debug.LogError($"���� ���̺� �ε��� {currentWaveIndex}�� NextWaveIndex ���� 0���� �۽��ϴ�.");
                    yield break;
                }
            }

            yield return null;

        }
    }

    Unit Spawn(EnemyName enemyName)
    {
        GameObject enemy = null;
        enemy = enemyPoolList[(int)enemyName - 1].Get();
        if (enemy == null)
        {
            return null;
        }
        
        SetPositionY(enemy);
        return enemy.GetComponent<Unit>();
    }
    private void SetPositionY(GameObject enemy)
    {
        for (int i = 0; i < isSpawned.Length; i++)
        {
            if (isSpawned[i] == null)
            {
                enemy.transform.position = new Vector3(transform.position.x, transform.position.y - i * 0.025f, transform.position.z);
                isSpawned[i] = enemy;
                enemy.GetComponent<SpriteRenderer>().sortingOrder = i;
                break;
            }
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

