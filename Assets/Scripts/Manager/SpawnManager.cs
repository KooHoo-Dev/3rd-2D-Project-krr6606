using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    CatSpawner spawner;
    public static SpawnManager Instance { get; private set; }
    public CatSpawnSlot[] spawnSlots;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

}
