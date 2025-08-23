using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Trash : InteractableObject
{
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                ClearOption();
                this.gameObject.SetActive(false);
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
            case 1:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }
}
