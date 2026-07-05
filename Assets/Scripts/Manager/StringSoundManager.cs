using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StringSoundManager : MonoBehaviour
{
    public static StringSoundManager Instance;

    private Dictionary<string, AudioClip> soundDict;  // SFX와 BGM을 저장할 Dictionary
    [field:SerializeField] public AudioSource sfxPlayer { get; private set; }                   // SFX 재생용 AudioSource
    [field: SerializeField] public AudioSource bgmPlayer { get; private set; }                   // BGM 재생용 AudioSource
    public AudioClip LastSfxClip;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips; // 오디오 클립 배열

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayBGM("Bgm-타이틀");
        bgmPlayer.Play();
    }
    private void Init()
    {
        soundDict = new Dictionary<string, AudioClip>();
        bgmPlayer.loop = true; // BGM은 기본적으로 반복 재생

        // Dictionary 초기화
        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }
    }

    // SFX 재생
    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            LastSfxClip = clip;
            sfxPlayer.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }
    public void StopSFX()
    {
        sfxPlayer.Stop();
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    // BGM 재생
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.Play();
            }
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }
}
