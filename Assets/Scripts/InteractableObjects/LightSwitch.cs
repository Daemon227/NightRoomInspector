using TMPro;
using UnityEngine;

public class LightSwitch : InteractableObject
{
    public GameObject lights;

    private void OnEnable()
    {
        EventManager.OnChangeDay += TurnOfLight;
    }
    
    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Turn_Light_On");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Turn_Light_Off");
                break;
            case 2:
                text.text = MultiLanguageManager.Instance.GetText("Leave");
                break;
        }
    }
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                if (!GameManager.Instance.turnOnLight)
                {
                    GameManager.Instance.turnOnLight = true;
                    lights.SetActive(true);
                }
                // set di chuyen
                GameManager.Instance.canMove = true;
                ClearOption();
                break;
            case 1:
                if (GameManager.Instance.turnOnLight)
                {
                    GameManager.Instance.turnOnLight = false;
                    lights.SetActive(false);
                }
                // set di chuyen
                GameManager.Instance.canMove = true;
                ClearOption();
                break;
            case 2:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }
    public void TurnOfLight()
    {
        if(GameManager.Instance.currentDay == 2)
        {
            GameManager.Instance.turnOnLight = false;
            lights.SetActive(false);
        }
        else
        {
            GameManager.Instance.turnOnLight = true;
        }
    }
}
