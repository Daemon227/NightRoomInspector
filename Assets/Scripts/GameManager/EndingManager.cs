using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public GameObject endingPanel;
    public TextMeshProUGUI title;
    public TextMeshProUGUI textUI;
    public Image image;
    public Button backButton;
    public List<EndingData> endingDatas;

     public int reportedIndex = 0;
     public bool interactWithNpc = false;
     public bool enteredHouse = false;
     public int lookOutSideIndex = 0;

    public int endingID = 0;
 
    public static EndingManager Instance;

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
    }
    public void ShowEnding()
    {
        if(reportedIndex >= 3)
        {
            // sang ngayf moi => trong phong toan canh sat -> bi bat luon :))
            Debug.Log("Ending 1");
            endingID = 1;
        }
        else
        {
            if (interactWithNpc)
            {
                if (enteredHouse)
                {
                    // sang ngay moi -> vao kiem tra toan rac -> khong con ai song
                    // kiem tra xong cac phong -> quay lai thi bi 1 thang kill luon
                    Debug.Log("Ending 2");
                    endingID = 2;
                }
                else
                {
                    // ra ve -> bi kill luon tu phia sau
                    Debug.Log("Ending 3");
                    endingID = 3;
                }
            }
            else
            {
                if(lookOutSideIndex == 3)
                {
                    //len mo check phong, mo cua so -> noi chuyen canh sat ->
                    //chay ve && k tuon tac -> hien canh sat bat trum
                    Debug.Log("Ending 4");
                    endingID = 4;
                }
                else
                {
                    //khong mo cua, khong tuong tac -> ra ve -> bi kill tu phia sau.
                    Debug.Log("Ending 5");
                    endingID = 5;
                }
            }
        }
        endingPanel.SetActive(true);
        StartCoroutine(ShowEndingText(endingDatas[endingID - 1]));
    }

    public IEnumerator ShowEndingText(EndingData ending)
    {
        for(int i =0; i< ending.contents.Length; i++)
        {
            textUI.text = ending.contents[i];
            if (i < ending.endingImgs.Count) image.sprite = ending.endingImgs[i];
            yield return new WaitUntil(() => Input.GetMouseButton(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        textUI.gameObject.SetActive(false);
        title.text = ending.title;
        title.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void BackHome()
    {
        Debug.Log("Back home, end game");
    }
}

[System.Serializable]
public class EndingData 
{
    public string title;
    public string[] contents;
    public List<Sprite> endingImgs;
}
