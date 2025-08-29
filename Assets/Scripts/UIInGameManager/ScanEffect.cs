using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScanEffect : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed;

    public Button closeButton;
    public GameObject bloodObject;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public IEnumerator ShootingEffect()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        while (rectTransform.anchoredPosition.y < endPos.y)
        {
            Vector2 moveDir = endPos - startPos;
            rectTransform.anchoredPosition += moveDir * speed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
       
        closeButton.gameObject.SetActive(true);
        
    }
}