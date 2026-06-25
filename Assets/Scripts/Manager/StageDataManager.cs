using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    public static StageDataManager Instance { get; private set; }
    [field: SerializeField] public List<_Stage> StageList { get; private set; } = new List<_Stage>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public _Stage GetStageData(StageName stageName)
    {
        return StageList.Find(stage => stage.Stage == stageName);
    }
}
