using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunAndShooting : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed;
    
    public Button closeButton;
    public GameObject bloodObject;

    Animator animatior;
    private void Start()
    {
        animatior = GetComponent<Animator>();
    }
    public void Shooting()
    { 
        closeButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Close");
        closeButton.gameObject.SetActive(false);
        bloodObject.SetActive(false);
        StartCoroutine(ShootingEffect());
    }
    public IEnumerator ShootingEffect()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        while (rectTransform.anchoredPosition.y < endPos.y)
        {
            Vector2 moveDir = endPos - startPos;     
            rectTransform.anchoredPosition += moveDir * speed * Time.deltaTime;
            yield return null;
        }
        animatior.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        bloodObject.SetActive(true);
        closeButton.gameObject.SetActive(true);  
        rectTransform.anchoredPosition = startPos;
    }
}
