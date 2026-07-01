using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserGameDataSO", menuName = "ScriptableObjects/UserGameDataSO", order = 1)]
public class UserGameDataSO : ScriptableObject
{
    public StageName maxclearedStage;
    public int currentEXP = 0;
    
    public int goldPerSecLevel = 1;
    public int maxGoldLevel = 1;
    public int skillDamageLevel = 1;
    public int skillCoolDownLevel = 1;
    public int skillRangeLevel = 1;
    public int EXPGetLevel = 1;
    public int HeroSpeedLevel = 1;
    public int HeroHealthLevel = 1;
    public int HeroDamageLevel = 1;
    public List<CatName> UnLockList;
    public CatName[] CatSlotArray;
}
