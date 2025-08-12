using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;
    public List<GameObject> roomOnFloor1;
    public List<GameObject> roomOnFloor2;

    public bool turnOnLight = false;
    public bool checkFullRoom = false;
    public bool reportToBoss = false;

    public bool reportedSomeone = false;
    public static GameManager Instance;

    public bool canMove = true;
    private void Awake()
    {
        if(Instance!= null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        EventManager.OnChangeDay += ChangeDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= ChangeDay;
    }
    public int GetTotalRoomNeedToCheck()
    {
        int total = 0;
        foreach(var room in roomOnFloor1)
        {
            if (room.GetComponent<DoorManager>().canOpen) total += 1;
        }
        foreach (var room in roomOnFloor2)
        {
            if (room.GetComponent<DoorManager>().canOpen) total += 1;
        }
        return total;
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
        turnOnLight = false;
        checkFullRoom = false;
        reportToBoss = false;
        currentDay += 1;
    }

}
