using System.IO;
using UnityEngine;

public class SettingDataLoader : MonoBehaviour
{
    public static SettingDataLoader Instance;
    public int resolutionIndex = 0;
    private void Awake()
    {
        if(Instance == null)
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
        if(File.Exists(Application.persistentDataPath +"/settings.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/settings.json");
            SettingData loadedSettings = JsonUtility.FromJson<SettingData>(json);
            AudioManager.Instance.SetMusicVolume(loadedSettings.musicVolume);
            AudioManager.Instance.SetSFXVolume(loadedSettings.sfxVolume);
            MultiLanguageManager.Instance.ChangeLanguage(loadedSettings.language);
            Debug.Log("Settings loaded from " + Application.persistentDataPath + "/settings.json");
        }
        else
        {
            Debug.LogWarning("No settings file found at " + Application.persistentDataPath + "/settings.json");
        }
    }
    public void SaveSettings()
    {
        float musicVolume = AudioManager.Instance.musicVolume;
        float sfxVolume = AudioManager.Instance.sfxVolume;
        string language = MultiLanguageManager.Instance.currentLanguage;
        SettingData settingData = new SettingData(musicVolume, sfxVolume, language);
        string json = JsonUtility.ToJson(settingData);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
        Debug.Log("Settings saved to " + Application.persistentDataPath + "/settings.json");
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


