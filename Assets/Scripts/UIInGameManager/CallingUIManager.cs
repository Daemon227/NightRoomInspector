using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class CallingUIManager : MonoBehaviour
{
    public GameObject callingPanel;
    public GameObject ringThePhonePanel;
    public GameObject buttonGroup1;
    public List<Button> buttons;
    public GameObject buttonPrefab;
    public GameObject buttonGroup2;
    public TextMeshProUGUI dialogText;

    private PhoneDialogue phoneDialogue;
    private string filename = "PhoneDialogues/";
  
    private void OnEnable()
    {
        EventManager.StartCalling += LoadingHanle;
    }
    private void OnDisable()
    {
        EventManager.StartCalling -= LoadingHanle;
    }

    private void Start()
    {
        FirstOptionsSettup();
    }
    public void FirstOptionsSettup()
    {
        //set text cho button
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Report_Floor_1");
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Report_Floor_2");
        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("No_Report");
        buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Wrong_Call");

        List<GameObject> rooms1 = GameManager.Instance.roomOnFloor1;
        List<GameObject> rooms2 = GameManager.Instance.roomOnFloor2;
        buttons[0].onClick.AddListener(() => ShowRoomOnFloor(rooms1));
        buttons[1].onClick.AddListener(() => ShowRoomOnFloor(rooms2));
        buttons[2].onClick.AddListener(NoReportSomeone);
        buttons[3].onClick.AddListener(ReportFalseAlarm);
    }
    public void LoadingHanle()
    {
        string fullFilename = filename + MultiLanguageManager.Instance.currentLanguage + "PhoneDialogue";
        LoadPhoneDialog(fullFilename);
        StartCoroutine(RingThePhone());
    }
    public IEnumerator RingThePhone()
    {
        // khong cho di chuyen
        GameManager.Instance.canMove = false;
        // bat animation rung dt
        ringThePhonePanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        ringThePhonePanel.SetActive(false);

        // hien hoi thoai.
        dialogText.transform.parent.gameObject.SetActive(true);
        int day = GameManager.Instance.currentDay;
        var dialog = phoneDialogue.phonedialog.FirstOrDefault(a => a.day == day);
        foreach(var s in dialog.greetings)
        {
            dialogText.text = s;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        buttonGroup1.SetActive(true);
    }
    public void LoadPhoneDialog(string filename)
    {
        if (phoneDialogue != null) return;
        var handle = Addressables.LoadAssetAsync<TextAsset>(filename);
        handle.Completed += (AsyncOperationHandle<TextAsset> task) =>
        {
            if(task.Status == AsyncOperationStatus.Succeeded)
            {
                string jsonText = task.Result.text;
                phoneDialogue = JsonUtility.FromJson<PhoneDialogue>(jsonText);
                Debug.Log("Phone Load Succeeded");
            }
        };
    }
      
    public void ShowRoomOnFloor(List<GameObject> rooms)
    {
        if (rooms.Count > 0)
        {
            buttonGroup2.SetActive(true);
            buttonGroup1.SetActive(false);
            // sinh ra button theo phong
            foreach (var room in rooms)
            {
                if (room.GetComponent<Door>().canOpen == false) continue;      
                GameObject b = Instantiate(buttonPrefab, buttonGroup2.transform);
                b.GetComponentInChildren<TextMeshProUGUI>().text = room.GetComponent<Door>().roomName;
                b.GetComponent<Button>().onClick.AddListener(() => ReportSomeOne(room));
            }
            // sinh ra nut thoat
            GameObject backButton = Instantiate(buttonPrefab, buttonGroup2.transform);
            string backText = MultiLanguageManager.Instance.GetText("ReThink");
            backButton.GetComponentInChildren<TextMeshProUGUI>().text = backText;
            backButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                buttonGroup1.SetActive(true);
                buttonGroup2.SetActive(false);
                ClearOption(buttonGroup2);
            });
        }
    }
    // Option1 - Bao cao ai do
    public void ReportSomeOne(GameObject room)
    {
        GameManager.Instance.reportedRoom = room;

        int day = GameManager.Instance.currentDay;
        var dialog = phoneDialogue.phonedialog.FirstOrDefault(a => a.day == day);
        StartCoroutine(ShowDialog(dialog.responseAffterReport));
        GameManager.Instance.reportToBoss = true;
        EventManager.OnAllMissionComleted?.Invoke();
    }
    
    // Option2 - Khong bao cao ai ca
    public void NoReportSomeone()
    {
        int day = GameManager.Instance.currentDay;
        var dialog = phoneDialogue.phonedialog.FirstOrDefault(a => a.day == day);
        StartCoroutine(ShowDialog(dialog.responseAffterNoReport));
        GameManager.Instance.reportToBoss = true;
        EventManager.OnAllMissionComleted?.Invoke();
    }
    //Option3 - thong bao goi nham
    public void ReportFalseAlarm()
    {
        int day = GameManager.Instance.currentDay;
        var dialog = phoneDialogue.phonedialog.FirstOrDefault(a => a.day == day);
        StartCoroutine(ShowDialog(dialog.signOff));
    }
    public IEnumerator ShowDialog(string[] strings)
    {
        dialogText.transform.parent.gameObject.SetActive(true);
        buttonGroup1.SetActive(false);
        buttonGroup2.SetActive(false);
        foreach (var response in strings)
        {
            dialogText.text = response;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        EndPhone();
    }
    public void ClearOption(GameObject buttonGroup)
    {
        foreach (Transform child in buttonGroup.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.Instance.canMove = true;
    }
    
    public void EndPhone()
    {
        ClearOption(buttonGroup2);
        buttonGroup1.SetActive(false);
        buttonGroup2.SetActive(false);
        dialogText.transform.parent.gameObject.SetActive(false);
        callingPanel.SetActive(false);

        // set di chuyen
        GameManager.Instance.canMove = true;
    }
}

[System.Serializable]
public class PhoneDialogue
{
    public List<PhoneDialogueByDay> phonedialog;
}

[System.Serializable]
public class PhoneDialogueByDay
{
    public int day;
    public string[] greetings; // loi chao hoi
    public string[] responseAffterReport; // tra loi sau khi bao cao 1 nguoi
    public string[] responseAffterNoReport; // tra loi cua boss khi khong bao cao ai
    public string[] signOff; // loi chao tam biet
}


