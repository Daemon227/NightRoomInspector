using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MultiLanguageManager : MonoBehaviour
{
    public static MultiLanguageManager Instance;
    public string currentLanguage = "en";
    public Dictionary<string, string> localizedText;

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
        SaveLanguageFileToPlayerPrefs();
        LoadLangue(currentLanguage);
    }
    /*public void LoadLangue(string currentLangue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        
#else
        string filePath = Path.Combine(Application.streamingAssetsPath, "language.json");
        if (!File.Exists(filePath))
        {
            Debug.Log("File language is null");
            return;
        }
        string json = File.ReadAllText(filePath);
        LanguageText languageText = JsonConvert.DeserializeObject<LanguageText>(json);

        localizedText = new Dictionary<string, string>();
        if(languageText.languages.ContainsKey(currentLangue))
        {
            foreach (var entry in languageText.languages[currentLangue])
            {
                localizedText[entry.Key] = entry.Value;
            }
        }
        else
        {
            Debug.LogWarning($"Language '{currentLangue}' not found. Falling back to English.");
            foreach (var entry in languageText.languages["vn"])
            {
                localizedText[entry.Key] = entry.Value;
            }
        }
#endif
    }*/

    public void LoadLangue(string currentLangue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    // Nếu chạy WebGL thì lấy dữ liệu từ PlayerPrefs
    string json = PlayerPrefs.GetString("languageData", "");
    if (string.IsNullOrEmpty(json))
    {
        Debug.Log("Language data in PlayerPrefs is null");
        localizedText = new Dictionary<string, string>();
        return;
    }

    LanguageText languageText = JsonConvert.DeserializeObject<LanguageText>(json);
    localizedText = new Dictionary<string, string>();

    if (languageText.languages.ContainsKey(currentLangue))
    {
        foreach (var entry in languageText.languages[currentLangue])
        {
            localizedText[entry.Key] = entry.Value;
        }
    }
    else
    {
        Debug.LogWarning($"Language '{currentLangue}' not found in PlayerPrefs. Falling back to Vietnamese.");
        foreach (var entry in languageText.languages["vn"])
        {
            localizedText[entry.Key] = entry.Value;
        }
    }
#else
        // Nếu chạy Editor hoặc Standalone thì đọc file trong StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, "language.json");
        if (!File.Exists(filePath))
        {
            Debug.Log("File language is null");
            return;
        }
        string json = File.ReadAllText(filePath);
        LanguageText languageText = JsonConvert.DeserializeObject<LanguageText>(json);

        localizedText = new Dictionary<string, string>();
        if (languageText.languages.ContainsKey(currentLangue))
        {
            foreach (var entry in languageText.languages[currentLangue])
            {
                localizedText[entry.Key] = entry.Value;
            }
        }
        else
        {
            Debug.LogWarning($"Language '{currentLangue}' not found. Falling back to Vietnamese.");
            foreach (var entry in languageText.languages["vn"])
            {
                localizedText[entry.Key] = entry.Value;
            }
        }
#endif
    }


    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : "this key is null";
    }

    public void ChangeLanguage(string newLanguage)
    {
        currentLanguage = newLanguage;
        LoadLangue(currentLanguage);
        EventManager.OnChangeLanguage?.Invoke();
    }

    public void SaveLanguageFileToPlayerPrefs()
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        string filePath = Path.Combine(Application.streamingAssetsPath, "language.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerPrefs.SetString("languageData", json);
            PlayerPrefs.Save();
            Debug.Log("Language data saved to PlayerPrefs.");
        }
        else
        {
            Debug.LogError("language.json not found in StreamingAssets!");
        }
#endif
    }

}

[System.Serializable]
public class LanguageText
{
    public Dictionary<string, Dictionary<string, string>> languages;
}
