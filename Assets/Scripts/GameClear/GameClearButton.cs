using UnityEngine;

public class GameClearButton : MonoBehaviour
{
    public void Cleared(int stageIndex)
    {
        _Stage stageData = StageDataManager.Instance.GetStageData((StageName)stageIndex);
        GameDataManager gameDataManager = GameDataManager.Instance;
        UserGameDataSO userGameData = gameDataManager.userGameDataSO;
        userGameData.currentEXP += stageData.DefaultEXPReward;
        StageName maxClearedStage = (StageName)Mathf.Max((int)userGameData.maxclearedStage, stageIndex);
        maxClearedStage = (StageName)Mathf.Clamp((int)maxClearedStage, 0, (int)StageName.End);
        TimeManager.Instance.NormalSpeedGame();
        StringSoundManager.Instance.StopBGM();
        StringSoundManager.Instance.PlaySFX("Snd-Å¬¸¯");
        switch (stageIndex)
        {
            case 0:
                if(stageData.IsCleared == false)
                {
                    stageData.IsCleared = true;
                    if(userGameData.UnLockList.Contains(CatName.Cat1) == false)
                        gameDataManager.UnlockCat(CatName.Cat1);
                    userGameData.maxclearedStage = maxClearedStage;
                }
                break;
            case 1:
                if(stageData.IsCleared == false)
                {
                    stageData.IsCleared = true;
                    if(userGameData.UnLockList.Contains(CatName.Cat2) == false)
                        gameDataManager.UnlockCat(CatName.Cat2);
                    userGameData.maxclearedStage = maxClearedStage;
                }
                break;
            case 2:
                if(stageData.IsCleared == false)
                {
                    stageData.IsCleared = true;
                    if(userGameData.UnLockList.Contains(CatName.Cat3) == false)
                        gameDataManager.UnlockCat(CatName.Cat3);
                    userGameData.maxclearedStage = maxClearedStage;
                }
                break;
            case 3:
                if (stageData.IsCleared == false)
                {
                    stageData.IsCleared = true;
                    if(userGameData.UnLockList.Contains(CatName.Cat4) == false)
                        gameDataManager.UnlockCat(CatName.Cat4);
                    userGameData.maxclearedStage = maxClearedStage;
                }
                break;
            case 4:
                if(stageData.IsCleared == false)
                {
                    stageData.IsCleared = true;
                    if(userGameData.UnLockList.Contains(CatName.Cat5) == false)
                        gameDataManager.UnlockCat(CatName.Cat5);
                    userGameData.maxclearedStage = maxClearedStage;
                }
                break;
            default:
                Debug.LogError("Invalid stage index: " + stageIndex);
                break;
        }
        stageData.IsCleared = true;
    }
}
