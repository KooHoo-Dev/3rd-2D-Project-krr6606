using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform PoolParnt;
    private GameObject[] isSpawned; // 각 y축 위치에 적이 이미 스폰되었는지 여부를 나타내는 배열
    [field: SerializeField] public List<Unit> enemyList = new List<Unit>();

    public List<UnitPool> enemyPoolList = new List<UnitPool>();
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
    private void Awake()
    {
        WaitSpawnFrameDelay = new WaitForSeconds(SpawnFrameDelay);
    }
    private void Start()
    {
        isSpawned = new GameObject[MaxSpawnCount];

        stage = StageDataManager.Instance.GetStageData(StageName.Stage1);

        waveList = stage.waves;

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyPoolList.Add(new UnitPool(enemyList[i].gameObject, PoolParnt));
        }
        stageManager = StageManager.Instance;
 
        StartCoroutine(WaveCo());
    }

    private IEnumerator WaveCo()
    {
        SpawnTime = waveList[currentWaveIndex].IsRandomTime ?
            Random.Range(waveList[currentWaveIndex].MinSpawnInterval, waveList[currentWaveIndex].MaxSpawnInterval)
            : waveList[currentWaveIndex].MinSpawnInterval;
        CurrentSpawnTime = SpawnTime;
        while (true)
        {
            if(stageManager.IsStageEnd())
            {
                yield break;
            }
            isSpawnStopped = PoolParnt.childCount >= MaxSpawnCount;
            // 웨이브 스폰이 중지되었는지 확인
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
                Debug.Log($"웨이브 {currentWaveIndex} 스폰 시작: {waveList[currentWaveIndex].EnemyName}, 스폰 수: {waveList[currentWaveIndex].SpawnCount}");

                for (int i = 0; i < waveList[currentWaveIndex].SpawnCount; i++)
                {
                    Spawn(waveList[currentWaveIndex].EnemyName);
                    Debug.Log($"웨이브 {currentWaveIndex} 스폰 중: {waveList[currentWaveIndex].EnemyName}, 스폰 수: {i + 1}/{waveList[currentWaveIndex].SpawnCount}");
                    yield return WaitSpawnFrameDelay;
                }
                currentWaveIndex = waveList[currentWaveIndex].NextWaveIndex;
                SpawnTime = waveList[currentWaveIndex].IsRandomTime
                            ? Random.Range(waveList[currentWaveIndex].MinSpawnInterval, waveList[currentWaveIndex].MaxSpawnInterval)
                            : waveList[currentWaveIndex].MinSpawnInterval;
                CurrentSpawnTime = SpawnTime;
                if (currentWaveIndex < 0)
                {
                    Debug.LogError($"현재 웨이브 인덱스 {currentWaveIndex}의 NextWaveIndex 값이 0보다 작습니다. 올바른 값을 설정해주세요.");
                }
            }
            yield return null;

        }
    }

    void Spawn(EnemyName enemyName)
    {
        GameObject enemy = null;
        switch (enemyName)
        {
            case EnemyName.None:
                break;
            case EnemyName.Dog:
                enemy = enemyPoolList[0].Get();
                break;
            case EnemyName.Snake:
                enemy = enemyPoolList[1].Get();
                break;
            case EnemyName.Those_Guys:
                enemy = enemyPoolList[2].Get();
                break;
        }
        if(enemy == null)
        {
            return;
        }
        SetPositionY(enemy);
    }
    private void SetPositionY(GameObject enemy)
    {
        for (int i = 0; i < isSpawned.Length; i++)
        {
            if (isSpawned[i] == null)
            {
                enemy.transform.position = new Vector3(transform.position.x,transform.position.y - i * 0.05f, transform.position.z);
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

