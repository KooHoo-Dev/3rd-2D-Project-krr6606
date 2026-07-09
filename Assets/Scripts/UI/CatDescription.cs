using TMPro;
using UnityEngine;
public class CatDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    Swipe_Slot swipe_Slot;
    int currentIndex;
    GameDataManager gameDataManager;
    public void Start()
    {
        swipe_Slot = FindAnyObjectByType<Swipe_Slot>();
        gameDataManager = GameDataManager.Instance;
    }
    public void Update()
    {
        if (swipe_Slot.currentIndex != currentIndex)
        {
            currentIndex = swipe_Slot.currentIndex;
            CatName catName = swipe_Slot.slotDataList[currentIndex].catName;
            if (catName != CatName.None)
            {

                SetDescription(GameDataManager.Instance.CatSOArray[(int)catName].unitdata.Description);
            }
            else
            {
                GameLevelDataName levelName = swipe_Slot.slotDataList[currentIndex].levelName;
                switch (levelName)
                {
                    case GameLevelDataName.goldPerSecLevel:
                        
                        SetDescription("일 고양이의 일 효율을 올리면\n머니의 증가 스피드가 빨라집니다.");
                        break;
                    case GameLevelDataName.maxGoldLevel:
                        SetDescription("일 고양이의 지갑 사이즈를 늘립니다\n전투 중에 모을 수 있는 머니가 증가합니다");
                        break;
                    case GameLevelDataName.skillDamageLevel:
                        SetDescription("스킬 데미지가 증가합니다.");
                        break;
                    case GameLevelDataName.skillCoolDownLevel:
                        SetDescription("스킬 시전까지\n스킬 충전 시간이 감소합니다.");
                        break;
                    case GameLevelDataName.skillRangeLevel:
                        SetDescription("스킬 범위가 증가합니다.");
                        break;
                    case GameLevelDataName.EXPGetLevel:
                        SetDescription("스테이지 클리어 시에 받을 수 있는\n 경험치를 상승시킬 수 있습니다.");
                        break;
                    case GameLevelDataName.HeroSpeedLevel:
                        SetDescription("영웅의 이동 속도가 상승합니다.");
                        break;
                    case GameLevelDataName.HeroHealthLevel:
                        SetDescription("영웅의 체력이 상승합니다.");
                        break;
                    case GameLevelDataName.HeroDamageLevel:
                        SetDescription("영웅의 공격력이 상승합니다.");
                        break;
                    case GameLevelDataName.SpawnCoolDownLevel:
                        SetDescription("유닛을 소환하는 쿨타임이 감소합니다.");
                        break;
                    default: break;
                }

            }
        }
    }
    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}
