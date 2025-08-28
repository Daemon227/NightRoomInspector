using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.VersionControl;
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
        LoadLangue(currentLanguage);
    }
    public void LoadLangue(string currentLangue)
    {
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
    }

    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : "this key is null";
    }
}

[System.Serializable]
public class LanguageText
{
    public Dictionary<string, Dictionary<string, string>> languages;
}
