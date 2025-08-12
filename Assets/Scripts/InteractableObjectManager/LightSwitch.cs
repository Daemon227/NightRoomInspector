using UnityEngine;

public class LightSwitch : InteractableObject
{
    public GameObject lights;
    public GameObject ghost;
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                if (!GameManager.Instance.turnOnLight)
                {
                    GameManager.Instance.turnOnLight = true;
                    lights.SetActive(true);
                    ghost.SetActive(false);
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
                    ghost.SetActive(true);
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
}
