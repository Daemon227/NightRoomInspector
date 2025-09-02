using TMPro;
using UnityEngine;

public class Phone : InteractableObject
{
    public GameObject CallingPanel;

    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Call");
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
                if (GameManager.Instance.checkFullRoom)
                {
                    if (GameManager.Instance.currentDay == 4)
                    {
                        string notification = MultiLanguageManager.Instance.GetText("N_NoOne_Answer");
                        EventManager.ShowNotification(notification);
                        GameManager.Instance.reportToBoss = true;
                        EventManager.OnAllMissionComleted?.Invoke();
                        ClearOption();
                        return;
                    }
                    if (GameManager.Instance.reportToBoss == false)
                    {
                        CallingPanel.SetActive(true);
                        EventManager.StartCalling?.Invoke();
                    }
                    else
                    {
                        string notification = MultiLanguageManager.Instance.GetText("N_Has_Reported");
                        EventManager.ShowNotification(notification);
                    }
                }
                else
                {
                    string notification = MultiLanguageManager.Instance.GetText("N_Not_Checked_All_Room");
                    EventManager.ShowNotification(notification);
                }
                ClearOption();
                break;
            case 1:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }

}
