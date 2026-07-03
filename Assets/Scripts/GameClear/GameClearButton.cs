using UnityEngine;

public class GameClearButton : MonoBehaviour
{
    public void Cleared(int stageIndex)
    {
        StageDataManager stageDataManager = StageDataManager.Instance;
        
        GameDataManager.Instance.userGameDataSO.currentEXP += stageDataManager.GetStageData((StageName)stageIndex).DefaultEXPReward;
        switch(stageIndex)
        {
            case 0:
                if(stageDataManager.GetStageData((StageName)stageIndex).IsCleared == false)
                {
                    stageDataManager.GetStageData((StageName)stageIndex).IsCleared = true;
                    GameDataManager.Instance.userGameDataSO.UnLockList.Add(CatName.Cat2);
                }
                break;
            case 1:
                if(stageDataManager.GetStageData((StageName)stageIndex).IsCleared == false)
                {
                    stageDataManager.GetStageData((StageName)stageIndex).IsCleared = true;
                    GameDataManager.Instance.userGameDataSO.UnLockList.Add(CatName.Cat3);
                }
                break;
            case 2:
                if (stageDataManager.GetStageData((StageName)stageIndex).IsCleared == false)
                {
                    stageDataManager.GetStageData((StageName)stageIndex).IsCleared = true;
                    GameDataManager.Instance.userGameDataSO.UnLockList.Add(CatName.Cat4);
                }
                break;
            case 3:
                if(stageDataManager.GetStageData((StageName)stageIndex).IsCleared == false)
                {
                    stageDataManager.GetStageData((StageName)stageIndex).IsCleared = true;
                    GameDataManager.Instance.userGameDataSO.UnLockList.Add(CatName.Cat5);
                }
                break;
            default:
                Debug.LogError("Invalid stage index: " + stageIndex);
                break;
        }
        stageDataManager.GetStageData((StageName)stageIndex).IsCleared = true;
    }
}
