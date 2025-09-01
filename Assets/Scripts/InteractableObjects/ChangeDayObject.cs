using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeDayObject : InteractableObject
{
    public GameObject changeDayPanel;
    public TextMeshProUGUI tmp;

    private void Start()
    {
        StartCoroutine(ShowFirstDayUI());
        Debug.Log("play UI");
    }
    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Button_GoHome");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Button_Wait");
                break;
        }
    }

    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                if (GameManager.Instance.CanChangeDay())
                {
                    if(GameManager.Instance.currentDay == 4)
                    {
                        Debug.Log("End Game");
                        EventManager.OnActiveEnding?.Invoke();
                    }
                    else
                    {
                        StartCoroutine(ChangeDayUI());
                        EventManager.OnChangeDay?.Invoke();
                    }             
                }
                else
                {
                    string notification = MultiLanguageManager.Instance.GetText("N_Mission_Not_Completed");
                    EventManager.ShowNotification?.Invoke(notification);
                }
                ClearOption();
                break;
            case 1:
                ClearOption();
                // set khong cho di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }

    public IEnumerator ChangeDayUI()
    {
        changeDayPanel.SetActive(true);
        string dayText = MultiLanguageManager.Instance.GetText("Day");
        tmp.text = $"{dayText} {GameManager.Instance.currentDay + 1}";
        yield return new WaitForSeconds(2f);
        changeDayPanel.SetActive(false);

        // set di chuyen
        GameManager.Instance.canMove = true;
    }

    public IEnumerator ShowFirstDayUI()
    {
        changeDayPanel.SetActive(true);
        string dayText = MultiLanguageManager.Instance.GetText("Day");
        tmp.text = $"{dayText} {GameManager.Instance.currentDay}";
        yield return new WaitForSeconds(2f);
        changeDayPanel.SetActive(false);

        // set di chuyen
        GameManager.Instance.canMove = true;
    }

}
