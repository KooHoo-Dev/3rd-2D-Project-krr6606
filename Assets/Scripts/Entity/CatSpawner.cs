using System.Collections.Generic;
using UnityEngine;
public class CatSpawner : MonoBehaviour
{
    public Transform PoolParnt;
    [field: SerializeField] public List<Unit> catList;

    public List<UnitPool> catPoolList;

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
    public void Spawn(CatName catName)
    {
        switch (catName)
        {
            case CatName.None:
                break;
            case CatName.Cat1:
                catPoolList[0].Get();
                break;
            case CatName.Cat2:
                catPoolList[1].Get();
                break;
            case CatName.Cat3:
                catPoolList[2].Get();
                break;
        }
    }
}

