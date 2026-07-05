using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    Slider masterSlider;
    void Start()
    {
        masterSlider = GetComponent<Slider>();
        masterSlider.onValueChanged.AddListener(StringSoundManager.Instance.SetMasterVolume);
    }

}
