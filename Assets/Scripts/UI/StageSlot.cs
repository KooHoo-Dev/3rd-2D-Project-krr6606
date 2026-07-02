using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public GameObject ClearedText;
    public StageName StageName;
    private void Start()
    {
        int maxclearedStage = (int)GameDataManager.Instance.userGameDataSO.maxclearedStage;
        if((int)StageName > maxclearedStage)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }


        if (StageDataManager.Instance.GetStageData(StageName).IsCleared)
        {
            ClearedText.SetActive(true);
        }
        else
        {
            ClearedText.SetActive(false);
        }
    }
}
