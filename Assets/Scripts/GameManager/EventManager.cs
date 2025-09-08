using System;
using UnityEngine;

public static class EventManager
{
    //event open the door
    public static Action<int> OpenTheDoor;
    public static Action OnConversation;

    //event when have a phone
    public static Action StartCalling;
    public static Action OnCalling;

    //event when complete a  mission
    public static Action OnAllRoomChecked;
    public static Action OnAllMissionComleted;

    //event when have a notification
    public static Action<string> ShowNotification;

    //event change day
    public static Action OnChangeDay;

    //event to active ending
    public static Action OnActiveEnding;

    //event to update UI
    public static Action OnChangeLanguage;
}
