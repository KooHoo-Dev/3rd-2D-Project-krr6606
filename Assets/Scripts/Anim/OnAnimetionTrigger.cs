using UnityEngine;

public class OnAnimetionTrigger : MonoBehaviour
{
    public void OnEndAnimationDust()
    {
        gameObject.SetActive(false);
    }
}
