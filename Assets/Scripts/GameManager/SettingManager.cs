using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Setting Slider")]
    public Slider musicSlider;
    public Slider sfxSlider;
    [Header("Setting Dropdown")]
    public TMP_Dropdown resolutionSetting;
    public TMP_Dropdown languageSetting;

    [Header("UI element")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI languageText;
    public Button close;

    public TutorialPanel tutorialPanel;
    public Button showTutorialButton;

    public Button saveAndExitButton;
    private void Start()
    {
        close.onClick.AddListener(CloseSetting);
        if(showTutorialButton != null)
        {
            showTutorialButton.onClick.AddListener(OpenTutorial);
        }
        musicSlider.onValueChanged.AddListener(MusicSetting);
        sfxSlider.onValueChanged.AddListener(SFXSetting);
        resolutionSetting.onValueChanged.AddListener(ResolutionSetting);
        languageSetting.onValueChanged.AddListener(LanguageSetting);
        if (saveAndExitButton != null)
        {
            saveAndExitButton.onClick.AddListener(SaveAndExitGame);
        }
        UpdateUI();

        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;
    }

    public void MusicSetting(float volume)
    {
        if(volume < 0.01f) volume = 0.01f; // Avoid log(0) error
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void SFXSetting(float volume)
    {
        if (volume < 0.01f) volume = 0.01f; // Avoid log(0) error
        AudioManager.Instance.SetSFXVolume(volume);
    }
    public void ResolutionSetting(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Debug.Log("Set to 1920x1080");
                SettingDataLoader.Instance.resolutionIndex = 0;
                break;;
            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("Set to 1280x720");
                SettingDataLoader.Instance.resolutionIndex = 1;
                break;
            case 2:
                Screen.SetResolution(1024, 576, true);
                Debug.Log("Set to 1024x576");
                SettingDataLoader.Instance.resolutionIndex = 2;
                break;
            case 3:
                Screen.SetResolution(640, 360, true);
                Debug.Log("Set to 640x360");
                SettingDataLoader.Instance.resolutionIndex = 3;
                break;
        }
    }

    public void LanguageSetting(int index)
    {
        switch (index)
        {
            case 0:
                MultiLanguageManager.Instance.ChangeLanguage("en");
                Debug.Log("Set to English");             
                UpdateUI();
                break;
            case 1:
                MultiLanguageManager.Instance.ChangeLanguage("vn");
                Debug.Log("Set to Vietnamese");
                UpdateUI();
                break;        
        }
    }

    public void UpdateUI()
    {
        title.text = MultiLanguageManager.Instance.GetText("Setting_Title");
        musicText.text = MultiLanguageManager.Instance.GetText("Setting_Music");
        sfxText.text = MultiLanguageManager.Instance.GetText("Setting_SFX");
        resolutionText.text = MultiLanguageManager.Instance.GetText("Setting_Resolution");
        languageText.text = MultiLanguageManager.Instance.GetText("Setting_Language");

        if(saveAndExitButton!= null)
        {
            saveAndExitButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_BackToMenu");
        }
        if(showTutorialButton!= null)
        {
            showTutorialButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Menu_Tutorial");
        }
        resolutionSetting.value = SettingDataLoader.Instance.resolutionIndex;
        int langIndex = MultiLanguageManager.Instance.currentLanguage == "en" ? 0 : 1;
        Debug.Log("Current Language Index: " + langIndex);
        languageSetting.value = langIndex;
    }

    public void CloseSetting()
    {
        close.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
    public void OpenTutorial()
    {
        if(tutorialPanel != null)
        {
            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.ActiveEvent();
            this.CloseSetting();
        }
    }
    public void SaveAndExitGame()
    {
        DataLoading.Instance.SaveGameData(0);
        SceneManager.LoadScene("MenuScene");
    }
}
