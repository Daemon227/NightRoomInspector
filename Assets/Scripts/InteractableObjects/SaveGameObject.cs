using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameObject : InteractableObject
{
    public GameObject savePanel;
    public TextMeshProUGUI titleText;
    public List<Button> saveButtons;
    public List<TextMeshProUGUI> saveTexts;
    public TextMeshProUGUI saveNote;
    public Button closeButton;

    private void OnEnable()
    {
        EventManager.OnChangeDay += AutoSaveFile;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= AutoSaveFile;
    }

    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Button_Save");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Leave");
                break;
        }
    }
    public override void HandleOption(int optionIndex)
    {
        closeButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Back");
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
        else if (optionIndex == 1) 
        { 
            ClearOption();
            // set di chuyen
            GameManager.Instance.canMove = true;
        }
    }

    public void FirstSettup()
    {
        titleText.text = MultiLanguageManager.Instance.GetText("Select_Slot_To_Save");
        saveNote.text = MultiLanguageManager.Instance.GetText("Save_Note");
        GameData[] gameDataList = DataLoading.Instance.gameDataList;
        for (int i = 0; i < saveButtons.Count; i++)
        {
            if(gameDataList[i] != null)
            {
                saveTexts[i].text = "Save " + (i + 1) + ": " + gameDataList[i].name;
            }
            else
            {
                string emptySlot = MultiLanguageManager.Instance.GetText("Empty_File");
                saveTexts[i].text = "Save " + (i + 1) + ": "+ emptySlot;
            }
            int index = i; // Capture the current value of i
            //gan chuc nang cho nut
            saveButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Save");
            saveButtons[i].onClick.RemoveAllListeners();
            saveButtons[i].onClick.AddListener(() =>
            {
                DataLoading.Instance.SaveGameData(index);
                saveTexts[index].text = "Save " + (index) + ": " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            });
        }
    }

    public void AutoSaveFile()
    {
        DataLoading.Instance.SaveGameData(0);
        saveTexts[0].text = "Save " + (0) + ": " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
}
