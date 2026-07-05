using System.Collections;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    SceneManagement SceneManagement;

    void Start()
    {
        SceneManagement = SceneManagement.Instance;
    }

    // Update is called once per frame
    public void ChangeScene(int sceneIndex)
    {
        StringSoundManager.Instance.PlaySFX("Snd-Å¬¸¯");

        SceneManagement.LoadScene(sceneIndex);

    }

}
