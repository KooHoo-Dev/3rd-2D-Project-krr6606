using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainLobbyTextBox : MonoBehaviour, IPointerDownHandler
{
    TMP_Text textBox;
    private int currentIndex = 0;
    void Awake()
    {
        // Initialization code here
        textBox = GetComponentInChildren<TMP_Text>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        int randomIndex = Random.Range(0, 7);
        while (randomIndex == currentIndex)
        {
            randomIndex = Random.Range(0, 7);
        }
        currentIndex = randomIndex;
        switch (randomIndex)
        {
            case 0:
                textBox.text = "스테이지를 처음 클리어 하면\r\n 새로운 냥코가 해금된다냥\r\n까먹지 말고 파워업하고\r\n캐릭터를 편성하라냥!\r\n";
                break;
            case 1:
                textBox.text = "냥냥";
                break;
            case 2:
                textBox.text = "버그는 개발자의 친구다냥..";
                break;
            case 3:
                textBox.text = "세계를 정복하는거다냥!";
                break;
            case 4:
                textBox.text = "통솔력이 적용되지 않는건\r\n 신경쓰지 말라냥~";
                break;
            case 5:
                textBox.text = "적을 물리치면 적에 따라 머니를 얻는다냥!";
                break;
            case 6:
                textBox.text = "캐릭터를 편성하는 방법은 드래그 앤 드롭이다냥!";
                break;
        }
    }
}
