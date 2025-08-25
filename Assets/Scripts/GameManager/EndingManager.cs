using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public List<EndingData> endingDatas;
    public GameObject endingPanel;
    public TextMeshProUGUI descriptionText;
    public Image endingImage;

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
            EventManager.ShowNotification?.Invoke("I can't open the door. What happen?");
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
                EventManager.ShowNotification?.Invoke("Wait, Somthing wrong?");
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
    
    public void BackHome()
    {
        Debug.Log("Back home, end game");
    }

    public IEnumerator StartEndingUI(int endingId)
    {
        EndingData endingData = endingDatas.Find(e => e.endingID == endingId);
        if (endingData == null)
        {
            Debug.LogError("Ending data not found for ending ID: " + endingId);
        }
        else
        {
            endingPanel.SetActive(true);
            for (int i = 0; i < endingData.descriptions.Length; i++)
            {
                descriptionText.text = "";
                descriptionText.text += endingData.descriptions[i] + "\n";
                endingImage.sprite = endingData.endingImages[i];
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            endingPanel.SetActive(false);

            resultPanel.SetActive(true);
            int[] result = GameManager.Instance.GetMonsterReportedIndex();
            resultText.text = $"You have reported {result[0]} rooms, in which {result[1]} rooms had monsters.";
        }

    }
}

[System.Serializable]
public class EndingData
{
    public int endingID;
    public string endingName;
    public string[] descriptions;
    public Sprite[] endingImages;
}
