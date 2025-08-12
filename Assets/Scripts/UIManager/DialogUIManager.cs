using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    public GameObject DoorUI;
    public GameObject conversationPanel;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI npcDialog;
    public Image npcAvatar;

    public GameObject buttonPrefab;
    public GameObject buttonGroup;

    private NPCData currentNPCData;
    private NPCDataLoader npcDataLoader;
    private string dialogAddress = "DialogFiles/Dialogue_NPC_";
    private string avatarAddress = "CharacterImgs/";

    private void OnEnable()
    {
        EventManager.OpenTheDoor += StartLoadNPCData;
        EventManager.OnConversation += HandleConversationAction;
    }
    private void OnDisable()
    {
        EventManager.OpenTheDoor -= StartLoadNPCData;
        EventManager.OnConversation -= HandleConversationAction;
    }
    private void Start()
    {
        npcDataLoader = GetComponentInParent<NPCDataLoader>();
    }
    public void StartLoadNPCData(int roomIndex)
    {
        //Load du lieu
        string fullDialogAddress = dialogAddress + roomIndex;
        npcDataLoader.LoadDialog(fullDialogAddress, data => {
            if (data != null)
            {
                currentNPCData = data;
                // bat panel conversation
                conversationPanel.SetActive(true);
            }
            else Debug.Log("Data is null");
        });

        ////CharacterImgs/NPC_Image_Id_1_day_1
        int day = GameManager.Instance.currentDay;// fix here
        Debug.Log(roomIndex);
        string fullAvatarAddress = $"{avatarAddress}NPC_Image_Id_{roomIndex}_day_{1}";
        npcDataLoader.LoadImage(fullAvatarAddress, newSprite =>
        {
            if(newSprite!= null)
            {
                npcAvatar.sprite = newSprite;
            }
            else
            {
                Debug.Log("sprite is null");
            }
        });
        
    }

    public void HandleConversationAction()
    {
        StartCoroutine(FirstSettup());
    }
    public IEnumerator FirstSettup()
    {
        if(currentNPCData!= null)
        {
            descriptionText.gameObject.SetActive(true);
            descriptionText.text = currentNPCData.description;
            yield return new WaitUntil(()=> Input.GetMouseButton(0));
            int day = GameManager.Instance.currentDay;
            ShowOption(day);
            descriptionText.gameObject.SetActive(false);
        }    
    }

    public void ShowOption(int currentDay)
    {
        // set khong cho di chuyen
        GameManager.Instance.canMove = false;
        foreach (var dialog in currentNPCData.dialogueByDay)
        {
            if(dialog.day == currentDay)
            {
                buttonGroup.gameObject.SetActive(true);
                foreach(var option in dialog.options)
                {
                    GameObject b = Instantiate(buttonPrefab, buttonGroup.transform);
                    b.GetComponentInChildren<TextMeshProUGUI>().text = option.playerText;
                    b.GetComponent<Button>().onClick.AddListener(() => SettupButton(option));
                }
            }
        }
    }
    public void ClearOption()
    {
        foreach(Transform child in buttonGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SettupButton(DialogueOption option)
    {
        StartCoroutine(ShowDialog(option));
    }

    public IEnumerator ShowDialog(DialogueOption option)
    {
        foreach (var text in option.npcTexts)
        {
            ClearOption();
            npcDialog.transform.parent.gameObject.SetActive(true);
            npcDialog.text = text;
            yield return new WaitUntil(() => Input.GetMouseButton(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        if (option.isExit)
        {
            npcDialog.transform.parent.gameObject.SetActive(false);
            conversationPanel.SetActive(false);

            GameManager.Instance.canMove = true;
        }
        else
        {  
            npcDialog.transform.parent.gameObject.SetActive(false);
            
            ShowOption(GameManager.Instance.currentDay);
        }
    }

    
}
