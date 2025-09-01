using TMPro;
using UnityEngine;

public class Scanmachine : InteractableObject
{
    public GameObject messagePanel;
    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Button_Pickup");
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
                GameManager.Instance.canScan = true;
                messagePanel.SetActive(true);
                messagePanel.GetComponent<MessagePanel>().SetUIContent(0);
                ClearOption();
                transform.parent.gameObject.SetActive(false);
                break;
            case 1:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }
}
