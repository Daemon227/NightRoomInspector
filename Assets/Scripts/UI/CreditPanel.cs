using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditPanel : MonoBehaviour, IMenu
{
    public TextMeshProUGUI gameDesignerText;
    public TextMeshProUGUI artistText;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI programmerText;
    public Button backButton;

    public void ActiveEvent()
    {
        backButton.onClick.AddListener(() => this.gameObject.SetActive(false));
        UpdateUI();
    }
    public void UpdateUI()
    {
        gameDesignerText.text = MultiLanguageManager.Instance.GetText("Designer_Text");
        programmerText.text = MultiLanguageManager.Instance.GetText("Programmer_Text");
        artistText.text = MultiLanguageManager.Instance.GetText("Artist_Text");
        musicText.text = MultiLanguageManager.Instance.GetText("Music_SFX_Text");

        backButton.GetComponent<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Back");
    }
}
