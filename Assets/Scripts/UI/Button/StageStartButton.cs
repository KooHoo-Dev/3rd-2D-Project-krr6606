using UnityEngine;
using UnityEngine.SceneManagement;
public class StageStartButton : MonoBehaviour
{
    Swipe_Slot swipe_Slot;

    private void Awake()
    {
        swipe_Slot = FindAnyObjectByType<Swipe_Slot>();
    }
    public void OnClickStartButton()
    {
        int targetIndex = swipe_Slot.targetIndex;
        SceneManagement.Instance.LoadScene( 5 + targetIndex);
    }
}
