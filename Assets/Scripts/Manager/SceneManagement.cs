using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }
    public Canvas loadingCanvas;
    public GameObject BattleStartPanel;
    public Image loadingPanel;
    public float fadeInTime = 0.6f;
    public float fadeOutTime = 0.22f;
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
        if (SceneIndex < 5)
        {
            LoadScene((SceneName)SceneIndex);
        }
        else
        {
            EnterBattle(SceneIndex);
        }
        
    }
    private void LoadScene(SceneName sceneName)
    {
        
        StartCoroutine(FadeIn(loadingPanel, fadeInTime, sceneName));

        
    }
    public void EnterBattle(int SceneIndex)
    {
        StartCoroutine(EnterBattleCoroutine(SceneIndex));
    }
    public IEnumerator EnterBattleCoroutine(int SceneIndex)
    {
        StringSoundManager.Instance.StopBGM();
        StringSoundManager.Instance.PlaySFX("Snd-전투시작");
        BattleStartPanel.SetActive(true);
        yield return new WaitForSeconds(StringSoundManager.Instance.LastSfxClip.length);
        BattleStartPanel.SetActive(false);

        LoadScene((SceneName)SceneIndex);
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
        StartCoroutine(FadeOut(loadingPanel, fadeOutTime));
        switch (sceneName)
        {
            case SceneName.StartScene:
                StringSoundManager.Instance.PlayBGM("Bgm-타이틀");
                break;
            case SceneName.MainLobbyScene:
                StringSoundManager.Instance.PlayBGM("Bgm-메인");
                break;
            case SceneName.PowerUpScene:

                break;
            case SceneName.ChapterPertyScene:
                break;
            case SceneName.StageMapScene:
                StringSoundManager.Instance.PlayBGM("Bgm-메인");
                break;
            case SceneName.Stage00Scene:
                StringSoundManager.Instance.PlayBGM("Bgm-전투-2");
                break;
            case SceneName.Stage01Scene:
                StringSoundManager.Instance.PlayBGM("Bgm-전투-2");
                break;
            case SceneName.Stage02Scene:
                StringSoundManager.Instance.PlayBGM("Bgm-전투-2");
                break;
            case SceneName.Stage03Scene:
                StringSoundManager.Instance.PlayBGM("Bgm-전투-2");
                break;
            case SceneName.Stage04Scene:
                StringSoundManager.Instance.PlayBGM("Bgm-전투-2");
                break;
        }

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
