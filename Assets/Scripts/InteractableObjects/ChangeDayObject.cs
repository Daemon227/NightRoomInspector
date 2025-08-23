using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeDayObject : InteractableObject
{
    public GameObject changeDayPanel;
    public TextMeshProUGUI tmp;
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
                    EventManager.ShowNotification?.Invoke("I must finish daily task first");
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
        tmp.text = $"Day: {GameManager.Instance.currentDay + 1}";
        yield return new WaitForSeconds(1.5f);
        changeDayPanel.SetActive(false);

        // set di chuyen
        GameManager.Instance.canMove = true;
    }
}
