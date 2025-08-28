using System;
using TMPro;
using UnityEngine;

public class Door : InteractableObject
{
    public GameObject DoorUI;
    public int roomIndex;
    public string roomName = "Phòng 1";
    public bool canOpen = true;
    public bool hasChecked = false;
    public bool isMonster = false;
    public override void SetButtonLanguage(TextMeshProUGUI text,int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Knock");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Leave");
                break;
        }
    }

    public override void HandleOption(int optionIndex)
    {
        hasChecked = true;
        switch (optionIndex)
        {
            case 0:
                if (!GameManager.Instance.turnOnLight)
                {
                    string notification = MultiLanguageManager.Instance.GetText("N_Turn_On_Light");
                    EventManager.ShowNotification?.Invoke(notification);
                }
                else
                {
                    if (canOpen)
                    {
                        DoorUI.SetActive(true);
                        EventManager.OpenTheDoor?.Invoke(roomIndex);
                    }
                    else
                    {
                        string notification = MultiLanguageManager.Instance.GetText("N_Empty_Room");
                        EventManager.ShowNotification?.Invoke(notification);
                    }
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

    private void OnEnable()
    {
        EventManager.OnChangeDay += Reset;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= Reset;
    }
    public void Reset()
    {
        hasChecked = false;
    }
}
