using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public GameObject ClearedText;
    public StageName StageName;
    private void Start()
    {
        if(StageDataManager.Instance.GetStageData(StageName).IsCleared)
        {
            ClearedText.SetActive(true);
        }
        else
        {
            ClearedText.SetActive(false);
        }
    }
}
