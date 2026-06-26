using UnityEngine;
public class CatSpawnSlotParent : MonoBehaviour
{
    public CatSpawnSlot[] catSpawnSlots;

    void Awake()
    {
        catSpawnSlots = GetComponentsInChildren<CatSpawnSlot>();
    }
    public void SetSpawnSlot(CatName catName,float spawnInterval,int cost, int slotindex)
    {
        catSpawnSlots[slotindex].InitCatSlot( catName, spawnInterval, cost);
    }
}