using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InforUI : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Button settingButton;
    public GameObject settingPanel;

    private void OnEnable()
    {
        EventManager.OnChangeLanguage += UpdateUI;
        EventManager.OnChangeDay += UpdateUIByDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeLanguage -= UpdateUI;
        EventManager.OnChangeDay -= UpdateUIByDay;
    }
    private void Start()
    {
        UpdateUI();
        settingButton.onClick.AddListener(PauseGame);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        settingPanel.SetActive(true);
    }

    public void UpdateUI()
    {
        title.text = MultiLanguageManager.Instance.GetText("Day") + " "+ GameManager.Instance.currentDay;
    }
    public void UpdateUIByDay()
    {
        title.text = MultiLanguageManager.Instance.GetText("Day") + " " + GameManager.Instance.currentDay + 1;
    }
}
