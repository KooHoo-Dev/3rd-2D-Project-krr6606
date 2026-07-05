using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
   Slider bgmSlider;
    void Start()
    {
        bgmSlider = GetComponent<Slider>();
        bgmSlider.onValueChanged.AddListener(StringSoundManager.Instance.SetBgmVolume);
    }

}
