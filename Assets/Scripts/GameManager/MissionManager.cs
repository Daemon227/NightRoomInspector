using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public TextMeshProUGUI missionText;

    private List<GameObject> checkedRooms = new List<GameObject>();
    private int totalRoom;

    private void Start()
    {
        totalRoom = GameManager.Instance.GetTotalRoomNeedToCheck();
        missionText.text = $"Rooms need to check: 0/{totalRoom}";
    }

    private void OnEnable()
    {
        EventManager.OnRoomChecked += CheckRoom;
        EventManager.OnChangeDay += ChangeNextDay;
        EventManager.OnAllMissionComleted += ChangeMission;
    }

    private void OnDisable()
    {
        EventManager.OnRoomChecked -= CheckRoom;
        EventManager.OnChangeDay -= ChangeNextDay;
        EventManager.OnAllMissionComleted -= ChangeMission;
    }
    public void CheckRoom(GameObject room)
    {
        if (!checkedRooms.Contains(room))
        {
            checkedRooms.Add(room);
            totalRoom = GameManager.Instance.GetTotalRoomNeedToCheck();
            missionText.text = $"Rooms need to check: {checkedRooms.Count}/{totalRoom}";
            Debug.Log("Total room need to check" + totalRoom);
            Debug.Log("Room Checked " + checkedRooms.Count);
            if(checkedRooms.Count == totalRoom)
            {
                GameManager.Instance.checkFullRoom = true;
                missionText.text = "Report to boss";               
            }
        }
    }

    public void ChangeMission()
    {
        missionText.text = "Go home";
    }

    public void ChangeNextDay()
    {
        checkedRooms.Clear();
        totalRoom = GameManager.Instance.GetTotalRoomNeedToCheck();
        missionText.text = $"Rooms need to check: 0/{totalRoom}";
    }
}
