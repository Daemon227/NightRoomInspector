using UnityEngine;

public class DoorManager : InteractableObject
{
    public GameObject DoorUI;
    public int roomIndex;
    public string roomName = "Phòng 1";
    public bool canOpen = true;
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                if (!GameManager.Instance.turnOnLight)
                {
                    EventManager.ShowNotification?.Invoke("I must turn on the light first");
                }
                else
                {
                    if (canOpen)
                    {
                        DoorUI.SetActive(true);
                        EventManager.OpenTheDoor?.Invoke(roomIndex);
                        EventManager.OnRoomChecked?.Invoke(this.gameObject);
                    }
                    else
                    {
                        Debug.Log("Phong nay lam gi co ai o");
                        EventManager.ShowNotification?.Invoke("The room is null");
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
}
