using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallImage : InteractableObject
{
    public GameObject panel;
    public Button closeButton;
    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Button_View");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Leave");
                break;
        }
    }
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex) 
        {
            case 0:
                panel.SetActive(true);
                if(panel.GetComponent<IMenu>() != null)
                {
                    panel.GetComponent<IMenu>().ActiveEvent();
                }         
                closeButton.onClick.AddListener(ClosePanel);
                ClearOption();
                break;
            case 1:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }    
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
        // set di chuyen
        GameManager.Instance.canMove = true;
    }
}
