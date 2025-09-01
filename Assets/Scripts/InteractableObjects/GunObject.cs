using TMPro;
using UnityEngine;

public class GunObject : InteractableObject
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
                GameManager.Instance.canShoot = true;
                messagePanel.SetActive(true);
                messagePanel.GetComponent<MessagePanel>().SetUIContent(1);
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
