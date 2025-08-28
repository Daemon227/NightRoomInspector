using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLoading : MonoBehaviour
{
    public static DataLoading Instance;
    public GameData currentGameData;
    public GameData[] gameDataList;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameDataList = LoadGameData();
        if (gameDataList == null)
        {
            gameDataList = new GameData[5];
        }
    }

    public void SaveGameData(int saveIndex)
    {
        GameData newGameData = new GameData();
        if (saveIndex < gameDataList.Length)
        {
            newGameData.FillFullData();
            gameDataList[saveIndex] = newGameData;
        }
        SaveDataWrapper saveDataWrapper = new SaveDataWrapper(gameDataList);

        string json = JsonUtility.ToJson(saveDataWrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame" + ".json", json);
        Debug.Log("Game data saved to " + Application.persistentDataPath + "/savegame" + ".json");
    }

    public GameData[] LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/savegame" + ".json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savegame" + ".json");
            SaveDataWrapper loadedData = JsonUtility.FromJson<SaveDataWrapper>(json);
            Debug.Log("Game data loaded from " + Application.persistentDataPath + "/savegame" + ".json");
            gameDataList = loadedData.gameDataArray;
            return gameDataList;
        }
        else
        {
            Debug.LogWarning("No save file found at " + Application.persistentDataPath + "/savegame" + ".json");
            return null;
        }

    }
}

[System.Serializable]
public class SaveDataWrapper
{
    public GameData[] gameDataArray;
    public SaveDataWrapper(GameData[] gameDataArray)
    {
        this.gameDataArray = gameDataArray;
    }
}

[System.Serializable]
public class GameData
{
    public string name;
    public int currentDay;
    public bool checkFullRoom;
    public bool reportToBoss;
    public bool[] roomsCanOpenFloor1 = new bool[4];
    public bool[] roomsCanOpenFloor2 = new bool[4];
    public bool[] roomsHasCheckedF1 = new bool[4];
    public bool[] roomsHasCheckedF2 = new bool[4];
    public bool[] roomIsMonsterF1 = new bool[4];
    public bool[] roomIsMonsterF2 = new bool[4];
    public int reportedRoomID;
    public bool canMove;
    public bool canInteract;

    public void FillFullData()
    {
        if(GameManager.Instance == null) return;
        name = "Save Time " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        currentDay = GameManager.Instance.currentDay;
        checkFullRoom = GameManager.Instance.checkFullRoom;
        reportToBoss = GameManager.Instance.reportToBoss;
        reportedRoomID = GameManager.Instance.reportedRoom != null ? GameManager.Instance.reportedRoom.GetComponent<Door>().roomIndex : -1;
        canInteract = GameManager.Instance.canInteract;

        int i = 0;
        foreach (var room in GameManager.Instance.roomOnFloor1)
        {      
            roomsCanOpenFloor1[i] = room.GetComponent<Door>().canOpen;
            roomsHasCheckedF1[i] = room.GetComponent<Door>().hasChecked;
            roomIsMonsterF1[i] = room.GetComponent<Door>().isMonster;
            i++;
        }
        i = 0;
        foreach (var room in GameManager.Instance.roomOnFloor2)
        {
            roomsCanOpenFloor2[i] = room.GetComponent<Door>().canOpen;
            roomsHasCheckedF2[i] = room.GetComponent<Door>().hasChecked;
            roomIsMonsterF2[i] = room.GetComponent<Door>().isMonster;
            i++;
        }
    }
}