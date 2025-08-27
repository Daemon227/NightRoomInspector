using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameObject : InteractableObject
{
    public GameObject savePanel;
    public List<Button> saveButtons;
    public List<TextMeshProUGUI> saveTexts;
    public Button closeButton;

    public override void HandleOption(int optionIndex)
    {
        if (optionIndex == 0) 
        { 
            savePanel.SetActive(true);
            ClearOption();
            FirstSettup();
            closeButton.onClick.AddListener(() => 
            {
                savePanel.SetActive(false);
                ClearOption();
                GameManager.Instance.canMove = true;
            });
        }
    }

    public void FirstSettup()
    {
        GameData[] gameDataList = DataLoading.Instance.gameDataList;
        for (int i = 0; i < saveButtons.Count; i++)
        {
            if(gameDataList[i] != null)
            {
                saveTexts[i].text = "Save " + (i + 1) + ": " + gameDataList[i].name;
            }
            else
            {
                saveTexts[i].text = "Save " + (i + 1) + ": Empty Slot";
            }
            int index = i; // Capture the current value of i
            //gan chuc nang cho nut
            saveButtons[i].onClick.RemoveAllListeners();
            saveButtons[i].onClick.AddListener(() =>
            {
                DataLoading.Instance.SaveGameData(index);
                saveTexts[index].text = "Save " + (index) + ": " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            });
        }
    }
}
