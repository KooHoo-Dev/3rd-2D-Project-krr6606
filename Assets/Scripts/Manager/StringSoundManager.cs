using UnityEngine;
using System.Collections.Generic;

public class StringSoundManager : MonoBehaviour
{
    public static StringSoundManager Instance;
    private Dictionary<string, AudioClip> soundDict;

    [field: SerializeField] public AudioSource sfxPlayer { get; private set; }
    [field: SerializeField] public AudioSource bgmPlayer { get; private set; }
    public AudioClip LastSfxClip;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips;

    [Header("Volume")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float bgmVolume = 1f;

    private const string MASTER_KEY = "MasterVolume";
    private const string SFX_KEY = "SfxVolume";
    private const string BGM_KEY = "BgmVolume";

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
            return;
        }
    }

    void Start()
    {
        LoadVolumeSettings();
        PlayBGM("Bgm-타이틀");
    }

    private void Init()
    {
        soundDict = new Dictionary<string, AudioClip>();
        bgmPlayer.loop = true;

        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }
    }

    // ---------------- SFX ----------------
    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            LastSfxClip = clip;
            sfxPlayer.PlayOneShot(clip, sfxVolume * masterVolume);
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }

    public void StopSFX() => sfxPlayer.Stop();

    // ---------------- BGM ----------------
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.volume = bgmVolume * masterVolume;
                bgmPlayer.Play();
            }
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }

    public void StopBGM() => bgmPlayer.Stop();

    // ---------------- Volume Control ----------------
    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        ApplyVolumes();
        SaveVolumeSettings();
    }

    public void SetSfxVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        SaveVolumeSettings();
    }

    public void SetBgmVolume(float value)
    {
        bgmVolume = Mathf.Clamp01(value);
        ApplyVolumes();
        SaveVolumeSettings();
    }

    private void ApplyVolumes()
    {
        // BGM은 실시간 반영 필요 (재생 중이므로)
        bgmPlayer.volume = bgmVolume * masterVolume;
        // SFX는 PlayOneShot 호출 시점에 반영되므로 여기선 별도 처리 불필요
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MASTER_KEY, masterVolume);
        PlayerPrefs.SetFloat(SFX_KEY, sfxVolume);
        PlayerPrefs.SetFloat(BGM_KEY, bgmVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        bgmVolume = PlayerPrefs.GetFloat(BGM_KEY, 1f);
        ApplyVolumes();
    }
}