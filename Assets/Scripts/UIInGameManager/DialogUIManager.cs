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

    [Header("Prepare for main option")]
    // for scan option
    public GameObject scanObject;
    public Image scanImage;
    public Button closeButton;
    //for shoot option
    public GameObject gunObject;
    public Button closeShootOption;

    private NPCData currentNPCData;
    private NPCDataLoader npcDataLoader;
    private string dialogAddress = "DialogFiles/Dialogue_NPC_";
    private string avatarAddress = "CharacterImgs/";

    private int currentRoom = 0;

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
        closeButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Close");
        closeButton.onClick.AddListener(() => CloseScan());
        closeShootOption.onClick.AddListener(() => CloseShootOption());
    }
    public void StartLoadNPCData(int roomIndex)
    {
        currentRoom = roomIndex;
        //Load dialog data
        string languageCode = MultiLanguageManager.Instance.currentLanguage;
        string fullDialogAddress = languageCode + dialogAddress + roomIndex;
        npcDataLoader.LoadDialog(fullDialogAddress, data => {
            if (data != null)
            {
                currentNPCData = data;
                // bat panel conversation
                conversationPanel.SetActive(true);

                //set ismonster vao door
                if (GameManager.Instance.GetRoomByID(roomIndex) != null)
                {
                    GameManager.Instance.GetRoomByID(roomIndex).isMonster = currentNPCData.isMonster;
                }
            }
            else Debug.Log("Data is null");
        });

        //CharacterImgs/NPC_Image_Id_1_day_1
        //Load avatar image
        int day = GameManager.Instance.currentDay;// fix here
        Debug.Log(roomIndex);
        string fullAvatarAddress = $"{avatarAddress}NPC_Image_Id_{roomIndex}_day_{day}";
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

        if (day <= 2) return;
        string fullScanAvatarAddress = $"{avatarAddress}NPC_Image_Id_{roomIndex}_day_{day}";// fix here
        npcDataLoader.LoadImage(fullScanAvatarAddress, newSprite =>
        {
            if (newSprite != null)
            {
                scanImage.sprite = newSprite;
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
            descriptionText.transform.parent.gameObject.SetActive(true);
            descriptionText.text = currentNPCData.description;
            yield return new WaitUntil(()=> Input.GetMouseButton(0));
            int day = GameManager.Instance.currentDay;
            ShowMainOption(day);
            descriptionText.transform.parent.gameObject.SetActive(false);
        }    
    }
    public void ShowMainOption(int day)
    {
        GameManager.Instance.canMove = false;

        GameObject b1 = Instantiate(buttonPrefab, buttonGroup.transform);
        string chatText = MultiLanguageManager.Instance.GetText("Chat");
        b1.GetComponentInChildren<TextMeshProUGUI>().text = chatText;
        b1.GetComponent<Button>().onClick.AddListener(() => ShowDialogueOption(day));

        if(day > 2)
        {
            GameObject b2 = Instantiate(buttonPrefab, buttonGroup.transform);
            string scanText = MultiLanguageManager.Instance.GetText("Scan");
            b2.GetComponentInChildren<TextMeshProUGUI>().text = scanText;
            b2.GetComponent<Button>().onClick.AddListener(Scan);
        }

        if (day > 3)
        {
            GameObject b3 = Instantiate(buttonPrefab, buttonGroup.transform);
            string shootText = MultiLanguageManager.Instance.GetText("Shoot");
            b3.GetComponentInChildren<TextMeshProUGUI>().text = shootText;
            b3.GetComponent<Button>().onClick.AddListener(Shoot);
        }

        GameObject b4 = Instantiate(buttonPrefab, buttonGroup.transform);
        string leaveText = MultiLanguageManager.Instance.GetText("Leave");
        b4.GetComponentInChildren<TextMeshProUGUI>().text = leaveText;
        b4.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClearOption();
            conversationPanel.SetActive(false);
            GameManager.Instance.canMove = true;
        });
    }
    public void ShowDialogueOption(int currentDay)
    { 
        ClearOption();
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
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        if (option.isExit)
        {
            npcDialog.transform.parent.gameObject.SetActive(false);
            ClearOption();
            ShowMainOption(GameManager.Instance.currentDay);
        }
        else
        {  
            npcDialog.transform.parent.gameObject.SetActive(false);
            
            ShowDialogueOption(GameManager.Instance.currentDay);
        }
    }

    //code for scan and shoot option
    public void Scan()
    {
        ClearOption();
        if (scanObject != null)
        {
            scanObject.SetActive(true);         
            GameManager.Instance.canMove = false;
        }
        else
        {
            Debug.Log("Scan object is not set");
        }
    }
    public void CloseScan()
    {
        if (scanObject != null)
        {
            scanObject.SetActive(false);
            GameManager.Instance.canMove = true;
        }
        else
        {
            Debug.Log("Scan object is not set");
        }
        ShowMainOption(GameManager.Instance.currentDay);
    }

    public void Shoot()
    {
        ClearOption();
        if (gunObject != null)
        {
            gunObject.SetActive(true);
            gunObject.GetComponentInChildren<GunAndShooting>().Shooting();
        }
        else
        {
            Debug.Log("Gun object is not set");
        }
    }
    public void CloseShootOption()
    {
        if (gunObject != null)
        {
            gunObject.SetActive(false);  
            closeShootOption.gameObject.SetActive(false);
            if (GameManager.Instance.GetRoomByID(currentRoom)!= null)
            {
                GameManager.Instance.GetRoomByID(currentRoom).canOpen = false;
            }
            conversationPanel.SetActive(false);
            GameManager.Instance.canMove = true;
            ClearOption();
        }
        else
        {
            Debug.Log("Gun object is not set");
        }
    }
}
