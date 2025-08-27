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
    }

    private void OnDisable()
    {
        EventManager.OpenTheDoor -= CheckRoom;
        EventManager.OnChangeDay -= ChangeNextDay;
        EventManager.OnAllMissionComleted -= ChangeMission;
    }
    public void CheckRoom(int id)
    {
        hasCheckedRooms += 1;
        missionText.text = $"Rooms need to check: {hasCheckedRooms}/{totalRoom}";     
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
            missionText.text = $"Rooms need to check: {hasCheckedRooms}/{totalRoom}";

        }
        else
        {
            if (!reportedToBoss)
            {
                missionText.text = "Report to boss";
            }
            else
            {
                missionText.text = "Go home";
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
