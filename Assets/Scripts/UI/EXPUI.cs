using UnityEngine;
using TMPro;
public class EXPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;

    void Start()
    {
        expText.text = GameDataManager.Instance.userGameDataSO.currentEXP.ToString();

    }

     public void UpdateEXPUI()
    {
        expText.text = GameDataManager.Instance.userGameDataSO.currentEXP.ToString();
    }
}
