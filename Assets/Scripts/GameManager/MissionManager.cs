using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public TextMeshProUGUI missionText;

    //private List<GameObject> checkedRooms = new List<GameObject>();
    private int totalRoom;
    private int hasCheckedRooms = 0;

    private bool checkedAllRooms = false;
    private bool reportedToBoss = false;

    private void Start()
    {
        ChangeMission();
    }

    private void OnEnable()
    {
        EventManager.OpenTheDoor += CheckRoom;
        EventManager.OnChangeDay += ChangeNextDay;
        EventManager.OnAllMissionComleted += ChangeMission;
        EventManager.OnChangeLanguage += ChangeMission;
    }

    private void OnDisable()
    {
        EventManager.OpenTheDoor -= CheckRoom;
        EventManager.OnChangeDay -= ChangeNextDay;
        EventManager.OnAllMissionComleted -= ChangeMission;
        EventManager.OnChangeLanguage -= ChangeMission;
    }
    public void CheckRoom(int id)
    {
        hasCheckedRooms += 1;
        missionText.text = $"{MultiLanguageManager.Instance.GetText("Mission_CheckRoom")} {hasCheckedRooms}/{totalRoom}";     
        if (hasCheckedRooms >= totalRoom)
        {
            checkedAllRooms = true;
            GameManager.Instance.checkFullRoom = true;
            ChangeMission();
        }
    }

    public void ChangeMission()
    {
        reportedToBoss = GameManager.Instance.reportToBoss;
        if (!checkedAllRooms)
        {
            totalRoom = GameManager.Instance.GetTotalRoomNeedToCheck();
            hasCheckedRooms = GameManager.Instance.GetRoomCheckedCount();
            missionText.text = $"{MultiLanguageManager.Instance.GetText("Mission_CheckRoom")} {hasCheckedRooms}/{totalRoom}";

        }
        else
        {
            if (!reportedToBoss)
            {
                missionText.text = MultiLanguageManager.Instance.GetText("Mission_Report");
            }
            else
            {
                missionText.text = MultiLanguageManager.Instance.GetText("Mission_Back");
            } 
        }
    }

    public void ChangeNextDay()
    {
        checkedAllRooms = false;
        reportedToBoss = false;
        ChangeMission();
    }
}
