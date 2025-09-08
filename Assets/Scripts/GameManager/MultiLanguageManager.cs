using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MultiLanguageManager : MonoBehaviour
{
    public static MultiLanguageManager Instance;
    public string currentLanguage = "en";
    public Dictionary<string, string> localizedText;
    private string fileName = "Language/language";
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
        localizedText = new Dictionary<string, string>();
        LoadLangue(currentLanguage);
    }

    public void LoadLangue(string currentLangue)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(fileName);
        handle.Completed += task =>
        {
            if(task.Status == AsyncOperationStatus.Succeeded)
            {
                string text = task.Result.text;
                LanguageText languageText = JsonConvert.DeserializeObject<LanguageText>(text);  
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
                EventManager.OnChangeLanguage?.Invoke();
            }
        };
    }

    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : "this key is null";
    }

    public void ChangeLanguage(string newLanguage)
    {
        currentLanguage = newLanguage;
        LoadLangue(currentLanguage);
    }

}

[System.Serializable]
public class LanguageText
{
    public Dictionary<string, Dictionary<string, string>> languages;
}
