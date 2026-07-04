using UnityEngine;

public class TutorialMainLobby : MonoBehaviour
{
    public GameObject CharacterPartyButton;
    public GameObject PowerUpButton;

    private void Start()
    {
        GameDataManager gameDataManager = GameDataManager.Instance;
        if ((int)gameDataManager.userGameDataSO.maxclearedStage < 1)
        {
            CharacterPartyButton.SetActive(false);
            PowerUpButton.SetActive(false);
        }
        else if ((int)gameDataManager.userGameDataSO.maxclearedStage >= 1)
        {
            CharacterPartyButton.SetActive(true);
            PowerUpButton.SetActive(true);
        }
    }
}

