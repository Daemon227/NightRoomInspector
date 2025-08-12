using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public GameObject notificationObject;
    
    public void TurnOnNotification()
    {
        notificationObject.SetActive(true);
    }
    public void TurnOfNotification()
    {
        notificationObject.SetActive(false);
    }


}
