using System.Collections;
using UnityEngine;

public class HiddenNPC : InteractableObject
{
    public override void HandleOption(int optionIndex)
    {
        EndingManager.Instance.interactWithNpc = true;
        switch (optionIndex)
        {
            case 0:               
                ClearOption();
                EventManager.ShowNotification?.Invoke("Hắn có chìa khóa vào đây à?, Hẳn là ông chủ rồi");
                EndingManager.Instance.enteredHouse = true;
                gameObject.SetActive(false);
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
            case 1:
                ClearOption();
                EventManager.ShowNotification?.Invoke("Sao hắn ta vào đươc đây nhỉ, thật kỳ lạ");
                EndingManager.Instance.enteredHouse = false;
                gameObject.SetActive(false);

                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }

}
