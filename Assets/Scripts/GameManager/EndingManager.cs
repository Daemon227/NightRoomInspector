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
    //set for first ending
    [Header("First Ending")]
    public GameObject firstEndingPanel;
    public GameObject fireObject;
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
                SecondEndingEvent();
                endingID = 2;
            }
            else
            {
                ThirdEndingEvent();
                endingID = 3;
            }
        }
    }

    public void FirstEndingEvent()
    {
        Debug.Log("Jump out the window");
    }
    public void SecondEndingEvent()
    {
        Debug.Log("Second ending event");
        EventManager.ShowNotification?.Invoke("Fire?");
    }
    public void ThirdEndingEvent()
    {
        Debug.Log("Third ending event");
        EventManager.ShowNotification?.Invoke("Everything end");
    }

    public void BackHome()
    {
        Debug.Log("Back home, end game");
    }
}
