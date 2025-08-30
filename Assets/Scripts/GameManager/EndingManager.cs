using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;
    public bool canFireMove = true;
    public int endingID = -1;

    [Header("Ending objects")]
    public GameObject fireObject;
    public GameObject monster;

    [Header("Ending Elements")]
    public List<CutSceneData> endingDatas;
    public GameObject endingPanel;
    public TextMeshProUGUI endingNameText;
    public TextMeshProUGUI descriptionText;
    public Image endingImage;
    public TextMeshProUGUI instructionText;

    [Header("Result Elements")]
    public GameObject resultPanel;
    public Button backButton;
    public TextMeshProUGUI resultText;
    private void OnEnable()
    {
        EventManager.OnActiveEnding += ShowEnding;
    }
    private void OnDisable()
    {
        EventManager.OnActiveEnding -= ShowEnding;
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
       //backButton.onClick.AddListener(BackHome);
    }
    public void ShowEnding()
    {
        // int[0]: tong so phong bi bao cao;   int[1]: so phong co quai bi bao cao
        int[] result = GameManager.Instance.GetMonsterReportedIndex();
        if (result[0] == 0)
        {
            Debug.Log("First ending event");
            string notification = MultiLanguageManager.Instance.GetText("N_Cannot_Open_Door");
            EventManager.ShowNotification?.Invoke(notification);
            fireObject.SetActive(true);
            endingID = 1;
        }
        else
        {
            if (result[1] < 4)
            {
                monster.SetActive(true);
                GameManager.Instance.canInteract = false;
                endingID = 2;
                string notification = MultiLanguageManager.Instance.GetText("N_Hear_Sound");
                EventManager.ShowNotification?.Invoke(notification);
            }
            else
            {   
                endingID = 3;
                GameManager.Instance.canInteract = false;
                StartEnding();
            }
        }
    }
    public void StartEnding()
    {
        StartCoroutine(StartEndingUI(endingID));
    }

    public IEnumerator StartEndingUI(int endingId)
    {
        CutSceneData endingData = endingDatas.Find(e => e.cutSceneID == endingId);
        if (endingData == null)
        {
            Debug.LogError("Ending data not found for ending ID: " + endingId);
        }
        else
        {
            endingPanel.SetActive(true);
            instructionText.text = MultiLanguageManager.Instance.GetText("Instruction_Click_To_Continue");
            string[] description;
            if (MultiLanguageManager.Instance.currentLanguage.Equals("vn"))
            {
                endingNameText.text = endingDatas[endingID].cutSceneVnName;                
                description = endingData.vnDescriptions;
            }
            else
            {
                endingNameText.text = endingDatas[endingID].cutSceneEngName;
                description= endingData.enDescriptions;
            }
            for (int i = 0; i < description.Length; i++)
            {
                descriptionText.text = "";
                descriptionText.text += description[i] + "\n";
                endingImage.sprite = endingData.images[i];
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            endingPanel.SetActive(false);

            resultPanel.SetActive(true);
            int[] result = GameManager.Instance.GetMonsterReportedIndex();
            string killCount = MultiLanguageManager.Instance.GetText("Result_Reported");
            string mistakeCount = MultiLanguageManager.Instance.GetText("Result_Mistaken");
            resultText.text = $"{killCount} {result[0]} \n" + $"{mistakeCount} {result[0] - result[1]}";
            backButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_BackToMenu");
            backButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayThemeMusic();
                SceneManager.LoadScene("MenuScene");
            });
        }

    }
}

[System.Serializable]
public class CutSceneData
{
    public int cutSceneID;
    public string cutSceneEngName;
    public string cutSceneVnName;
    public string[] enDescriptions;
    public string[] vnDescriptions;
    public Sprite[] images;
}
