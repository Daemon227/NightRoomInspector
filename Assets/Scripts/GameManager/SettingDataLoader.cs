using System.IO;
using UnityEngine;

public class SettingDataLoader : MonoBehaviour
{
    public static SettingDataLoader Instance;
    public int resolutionIndex = 0;
    private string settingFileName = "settings.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSetting();
    }

    private void OnEnable()
    {
        EventManager.OnChangeDay += SaveSettings;
    }

    private void OnDisable()
    {
        EventManager.OnChangeDay -= SaveSettings;
    }

    public void LoadSetting()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerPrefs.HasKey("settings"))
        {
            string json = PlayerPrefs.GetString("settings");
            SettingData loadedSettings = JsonUtility.FromJson<SettingData>(json);
            ApplySettings(loadedSettings);
            Debug.Log("Settings loaded from PlayerPrefs (WebGL).");
        }
        else
        {
            Debug.LogWarning("No settings found in PlayerPrefs (WebGL).");
        }
#else
        string path = Path.Combine(Application.persistentDataPath, settingFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingData loadedSettings = JsonUtility.FromJson<SettingData>(json);
            ApplySettings(loadedSettings);
            Debug.Log("Settings loaded from " + path);
        }
        else
        {
            Debug.LogWarning("No settings file found at " + path);
        }
#endif
    }

    public void SaveSettings()
    {
        float musicVolume = AudioManager.Instance.musicVolume;
        float sfxVolume = AudioManager.Instance.sfxVolume;
        string language = MultiLanguageManager.Instance.currentLanguage;

        SettingData settingData = new SettingData(musicVolume, sfxVolume, language);
        string json = JsonUtility.ToJson(settingData);

#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerPrefs.SetString("settings", json);
        PlayerPrefs.Save();
        Debug.Log("Settings saved to PlayerPrefs (WebGL).");
#else
        string path = Path.Combine(Application.persistentDataPath, settingFileName);
        File.WriteAllText(path, json);
        Debug.Log("Settings saved to " + path);
#endif
    }

    private void ApplySettings(SettingData data)
    {
        AudioManager.Instance.SetMusicVolume(data.musicVolume);
        AudioManager.Instance.SetSFXVolume(data.sfxVolume);
        MultiLanguageManager.Instance.ChangeLanguage(data.language);
    }
}

[System.Serializable]
public class SettingData
{
    public float musicVolume;
    public float sfxVolume;
    public string language;

    public SettingData(float musicVolume, float sfxVolume, string language)
    {
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
        this.language = language;
    }
}
