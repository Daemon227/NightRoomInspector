using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadFileSaveUI: MonoBehaviour,IMenu
{
    public GameObject mainPanel;
    public List<Button> LoadButtons;
    public List<TextMeshProUGUI> filesaveName;
    public Button backButton;

    public GameObject loadingPanel;
    public void Start()
    {
        backButton.onClick.AddListener(() => {
            mainPanel.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }
    public void ActiveEvent()
    {
        GameData[] gameDataList = DataLoading.Instance.gameDataList;
        Debug.Log("Game data list length: " + gameDataList.Length);
        for (int i = 0; i < gameDataList.Length; i++)
        {
            if (gameDataList[i] != null)
            {
                filesaveName[i].text = gameDataList[i].name;
                int index = i; // Capture the current value of i     
                LoadButtons[index].onClick.AddListener(() => {
                    DataLoading.Instance.currentGameData = gameDataList[index];
                    StartCoroutine(LoadGame());
                });
            }
            else
            {
                filesaveName[i].text = "Empty Slot";
                LoadButtons[i].interactable = false;               
            }
        }
    }

    public void OpenPanel()
    {
        mainPanel.SetActive(true);
    }

    public IEnumerator LoadGame()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
