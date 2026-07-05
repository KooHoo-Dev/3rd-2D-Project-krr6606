using UnityEngine;

public class VolumeSettingButton : MonoBehaviour
{
    public GameObject volumeSettingPanel;

    public void OnClick()
    {
        volumeSettingPanel.SetActive(true);
    }

}
