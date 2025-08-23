using UnityEngine;
using UnityEngine.UI;

public class WallImage : InteractableObject
{
    public GameObject panel;
    public Button closeButton;
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex) 
        {
            case 0:
                panel.SetActive(true);
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
