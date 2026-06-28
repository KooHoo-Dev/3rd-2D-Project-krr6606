using System.Collections.Generic;
using UnityEngine;
public class CatSpawner : MonoBehaviour
{
    public Transform PoolParnt;
    public GameObject[] isSpawned; // 각 y축 위치에 적이 이미 스폰되었는지 여부를 나타내는 배열
    [field: SerializeField] public List<Unit> catList;

    public List<UnitPool> catPoolList;
    public int MaxSpawnCount = 50;
    public bool isSpawnStopped = false;
    public float SpawnFrameDelay = 0.2f;

    void Start()
    {
        catPoolList = new List<UnitPool>();

        isSpawned = new GameObject[MaxSpawnCount];
        for (int i = 0; i < catList.Count; i++)
        {
            catPoolList.Add(new UnitPool(catList[i].gameObject, PoolParnt));
        }
    }
    public void Spawn(CatName catName)
    {
        isSpawnStopped = PoolParnt.childCount >= 50;
        if (isSpawnStopped)
        {
            return;
        }
        GameObject cat = null;
        switch (catName)
        {
            case CatName.None:
                break;
            case CatName.Cat1:
                cat = catPoolList[0].Get();
                break;
            case CatName.Cat2:
                cat = catPoolList[1].Get();
                break;
            case CatName.Cat3:
                cat = catPoolList[2].Get();
                break;
        }
        if (cat == null)
        {
           return;
        }
        SetPositionY(cat);

    }
    private void SetPositionY(GameObject cat)
    {

        for (int i = 0; i < isSpawned.Length; i++)
        {
            if (isSpawned[i] == null)
            {
                cat.transform.position = new Vector3(transform.position.x, transform.position.y - i * 0.05f, transform.position.z);
                isSpawned[i] = cat;
                cat.GetComponent<SpriteRenderer>().sortingOrder = i;
                break;
            }
        }
    }
}

