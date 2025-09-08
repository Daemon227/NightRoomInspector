using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;
    public List<GameObject> roomOnFloor1;
    public List<GameObject> roomOnFloor2;

    //biến quản lý sự kiện game
    public bool turnOnLight = false;
    public bool checkFullRoom = false;
    public bool reportToBoss = false;
    public GameObject reportedRoom = null;
    public bool canMove = true;
    public bool canInteract = true;
    //biến quản lý điều kiện game
    public bool canScan = false;
    public bool canShoot = false;


    public static GameManager Instance;
    private void Awake()
    {
        if(Instance!= null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameData data = DataLoading.Instance.currentGameData;
        LoadGame(data);
    }

    private void OnEnable()
    {
        EventManager.OnChangeDay += ChangeDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= ChangeDay;
    }

    public int GetTotalRoomNeedCheck()
    {
        int totalRoom = 0;
        foreach (var room in roomOnFloor1)
        {
            if (room.GetComponent<Door>().canOpen) totalRoom += 1;
        }
        foreach (var room in roomOnFloor2)
        {
            if (room.GetComponent<Door>().canOpen) totalRoom += 1;
        }
        return totalRoom;
    }
    public int GetRoomCheckedCount()
    {
        int checkedCount = 0;
        foreach (var room in roomOnFloor1)
        {
            if (room.GetComponent<Door>().canOpen && room.GetComponent<Door>().hasChecked) checkedCount += 1;
        }
        foreach (var room in roomOnFloor2)
        {
            if (room.GetComponent<Door>().canOpen && room.GetComponent<Door>().hasChecked) checkedCount += 1;
        }
        return checkedCount;
    }
    public bool CanChangeDay()
    {
        if (checkFullRoom && reportToBoss)
        {
            return true;
        }
        else return false;
    }
    public void ChangeDay()
    {
        //reset
        if(reportedRoom!= null)
        {
            Door door = reportedRoom.GetComponent<Door>();
            door.canOpen = false;
            door.SetDoorSprite();
            reportedRoom = null;
        }
        foreach(var room in roomOnFloor1)
        {
            room.GetComponent<Door>().hasChecked = false;
        }
        foreach(var room in roomOnFloor2)
        {
            room.GetComponent<Door>().hasChecked = false;
        }
        checkFullRoom = false;
        reportToBoss = false; 
        currentDay += 1;
    }

    public Door GetRoomByID(int id)
    {
        Door door = null;
        foreach(var room in roomOnFloor1)
        {
            if(room.GetComponent<Door>().roomIndex == id)
            {
                door = room.GetComponent<Door>();
                break;
            }
        }
        foreach (var room in roomOnFloor2)
        {
            if (room.GetComponent<Door>().roomIndex == id)
            {
                door = room.GetComponent<Door>();
                break;
            }
        }
        return door;
    }

    public int[] GetMonsterReportedIndex()
    {
        int roomReportedCount = 0;
        int monsterReportdCount = 0;
        foreach(var room in roomOnFloor1)
        {
            if(!room.GetComponent<Door>().canOpen)
            {
                roomReportedCount += 1;       
                if(room.GetComponent<Door>().isMonster) monsterReportdCount += 1;
            }
        }
        foreach (var room in roomOnFloor2)
        {
            if (!room.GetComponent<Door>().canOpen)
            {
                roomReportedCount += 1;       
                if (room.GetComponent<Door>().isMonster) monsterReportdCount += 1;
            }
        }
        int[] result = new int[2];
        result[0] = roomReportedCount;
        result[1] = monsterReportdCount;
        return result;
    }

    public void LoadGame(GameData data)
    {
        if(data == null) return;
        currentDay = data.currentDay;
        checkFullRoom = data.checkFullRoom;
        reportToBoss = data.reportToBoss;
        canMove = true;
        canInteract = data.canInteract;
        if(data.reportedRoomID != -1)
        {
            reportedRoom = GetRoomByID(data.reportedRoomID).gameObject;
        }
        else
        {
            reportedRoom = null;
        }
        for (int i =0; i<4; i++)
        {
            roomOnFloor1[i].GetComponent<Door>().canOpen = data.roomsCanOpenFloor1[i];
            roomOnFloor2[i].GetComponent<Door>().canOpen = data.roomsCanOpenFloor2[i];
            roomOnFloor1[i].GetComponent<Door>().hasChecked = data.roomsHasCheckedF1[i];
            roomOnFloor2[i].GetComponent<Door>().hasChecked = data.roomsHasCheckedF2[i];
            roomOnFloor1[i].GetComponent<Door>().isMonster = data.roomIsMonsterF1[i];
            roomOnFloor2[i].GetComponent<Door>().isMonster = data.roomIsMonsterF2[i];
        }
        canScan = data.canScan;
        canShoot = data.canShoot;
    }
}
