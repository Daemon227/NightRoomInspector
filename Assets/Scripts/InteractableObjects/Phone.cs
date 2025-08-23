using UnityEngine;

public class Phone : InteractableObject
{
    public GameObject CallingPanel;
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                if (GameManager.Instance.checkFullRoom)
                {
                    if (GameManager.Instance.reportToBoss == false)
                    {
                        CallingPanel.SetActive(true);
                        EventManager.StartCalling?.Invoke();
                    }
                    else
                    {
                        EventManager.ShowNotification("I reported");
                    }
                }
                else
                {
                    EventManager.ShowNotification("I must check all rooms first");
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
