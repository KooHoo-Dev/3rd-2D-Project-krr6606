using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class SpeedUpButton : MonoBehaviour
{
    public Image speedUpButtonImage;
    public void SpeedUpGame()
    {
        TimeManager.Instance.SpeedUpGame();
        speedUpButtonImage.color = Color.white;
    }
    public void NormalSpeedGame()
    {
        TimeManager.Instance.NormalSpeedGame();
        speedUpButtonImage.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void ToggleSpeedUpGame()
    {
        StringSoundManager.Instance.PlaySFX("Snd-Å¬¸¯");
        if (TimeManager.Instance.isSpeedUp)
        {
            NormalSpeedGame();
        }
        else
        {
            SpeedUpGame();
        }
    }
}
