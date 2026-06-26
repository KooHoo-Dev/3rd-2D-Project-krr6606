using System.Collections.Generic;
using UnityEngine;
public class CatSpawner : MonoBehaviour
{
    public Transform PoolParnt;
    [field: SerializeField] public List<Unit> catList;

    public List<UnitPool> catPoolList;
    
    private float SpawnTime;

    private float CurrentSpawnTime;

    private int currentWaveIndex;
    public bool isSpawnStopped = false;
    public float SpawnFrameDelay = 0.2f;

    void Start()
    {
        catPoolList = new List<UnitPool>();
        for (int i = 0; i < catList.Count; i++)
        {
            catPoolList.Add(new UnitPool(catList[i].gameObject, PoolParnt));
        }
    }
    void Spawn(EnemyName enemyName)
    {
        switch (enemyName)
        {
            case EnemyName.None:
                break;
            case EnemyName.Dog:
                catPoolList[0].Get();
                break;
            case EnemyName.Snake:
                catPoolList[1].Get();
                break;
            case EnemyName.Those_Guys:
                catPoolList[2].Get();
                break;
        }
    }
}

