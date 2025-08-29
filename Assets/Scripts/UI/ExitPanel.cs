using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        SettupButtonLanguage();
        yesButton.onClick.AddListener(() => StartCoroutine(ExitGame()));
        noButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
    private void OnEnable()
    {
        EventManager.OnChangeLanguage += SettupButtonLanguage;
    }
    private void OnDisable()
    {
        EventManager.OnChangeLanguage -= SettupButtonLanguage;
    }
    public IEnumerator ExitGame()
    {
        SettingDataLoader.Instance.SaveSettings();
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }

    public void SettupButtonLanguage()
    {
        title.text = MultiLanguageManager.Instance.GetText("Menu_Exit_Title");
        yesButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Exit_Yes");
        noButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Exit_No");
    }
}
