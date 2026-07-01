using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }
    public Canvas loadingCanvas;

    public Image loadingPanel;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(loadingCanvas);
        }
    }
    public void GameQuit()
    {

        Application.Quit();
    }
    public void LoadScene(int SceneIndex)
    {
       LoadScene((SceneName)SceneIndex);
    }
    private void LoadScene(SceneName sceneName)
    {
        
        Debug.Log($"씬 로드: {sceneName}");
        StartCoroutine(FadeIn(loadingPanel, 1, sceneName));
        
        
    }

    IEnumerator FadeIn(Image panel, float time, SceneName sceneName)
    {
        loadingCanvas.enabled = true;
        Debug.Log("FadeIn 시작");
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / time);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);

            yield return null;
        }

        SceneManager.LoadScene((int)sceneName);
        StartCoroutine(FadeOut(loadingPanel, 1));
    }
    IEnumerator FadeOut(Image panel, float time)
    {
        loadingCanvas.enabled = true;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / time));
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            yield return null;
        }
        loadingCanvas.enabled = false;

    }
}
