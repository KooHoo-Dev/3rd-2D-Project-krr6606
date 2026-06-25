using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [field: SerializeField] public List<Unit> catList = new List<Unit>();

    [field: SerializeField] public List<Unit> enemyList = new List<Unit>();

    public List<ComponentPool<Unit>> catPoolList = new List<ComponentPool<Unit>>();
    public List<ComponentPool<Unit>> enemyPoolList = new List<ComponentPool<Unit>>();
    _Stage _Stage;
    List<StageWaveData> _waveList;
    private float SpawnTime;

    private float CurrentSpawnTime;

    private void Awake()
    {
    }
    private void Start()
    {
        _Stage = StageDataManager.Instance.GetStageData(StageName.Stage1);

        _waveList = _Stage.waves;

        for (int i = 0; i < catList.Count; i++)
        {
            catPoolList.Add(new ComponentPool<Unit>(catList[i], transform));
        }

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyPoolList.Add(new ComponentPool<Unit>(enemyList[i], transform));
        }
    }
    public void Spawn(CatName catName)
    {

    }
    void Spawn(EnemyName enemyName)
    {

    }
}
