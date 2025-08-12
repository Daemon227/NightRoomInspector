using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public GameObject panel;

    public Image playerImage;
    public List<Sprite> sprites;

    public TextMeshProUGUI notification;

    private void OnEnable()
    {
        EventManager.ShowNotification += ShowNotification;
    }
    private void OnDisable()
    {
        EventManager.ShowNotification -= ShowNotification;
    }
    public void ShowNotification(string n)
    {
        StartCoroutine(ShowNotificationHandle(n));
    }
    public IEnumerator ShowNotificationHandle(string n)
    {
        // set di chuyen
        GameManager.Instance.canMove = false;

        panel.SetActive(true);
        notification.text = n;
        int day = GameManager.Instance.currentDay - 1;
        if(day<= sprites.Count) playerImage.sprite = sprites[day];

        float startTime = Time.time;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)|| (Time.time - startTime > 3));

        panel.SetActive(false);
        // set di chuyen
        GameManager.Instance.canMove = true;
    }
}
