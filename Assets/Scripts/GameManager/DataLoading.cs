using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLoading : MonoBehaviour
{
    public static DataLoading Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameData[] gameDataList = new GameData[4];
    public void SaveGameData(GameManager gameManager, int saveIndex)
    {
        GameData newGameData = new GameData(gameManager);
        if (saveIndex <= gameDataList.Length)
        {
            gameDataList[saveIndex - 1] = newGameData;
        }

        string json = JsonUtility.ToJson(newGameData, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame" + ".json", json);
        Debug.Log("Game data saved to " + Application.persistentDataPath + "/savegame" + ".json");
    }

    public GameData LoadGameData(int loadIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/savegame" + ".json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savegame" + ".json");
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game data loaded from " + Application.persistentDataPath + "/savegame" + ".json");
            return loadedData;
        }
        else
        {
            Debug.LogWarning("No save file found at " + Application.persistentDataPath + "/savegame" + ".json");
            return null;
        }

    }
}

[System.Serializable]
public class GameData
{
    public int currentDay;
    public bool checkFullRoom;
    public bool reportToBoss;
    public int reportedRoomID;
    public bool canMove;
    public bool canInteract;
    public GameData(GameManager gameManager)
    {
        currentDay = gameManager.currentDay;
        checkFullRoom = gameManager.checkFullRoom;
        reportToBoss = gameManager.reportToBoss;
        reportedRoomID = gameManager.reportedRoom != null ? gameManager.reportedRoom.GetComponent<Door>().roomIndex : -1;
        canInteract = gameManager.canInteract;
    }
}