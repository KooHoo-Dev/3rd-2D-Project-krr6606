using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
    Slider sfxSlider;
    void Start()
    {
        sfxSlider = GetComponent<Slider>();
        sfxSlider.onValueChanged.AddListener(StringSoundManager.Instance.SetSfxVolume);
    }


}
