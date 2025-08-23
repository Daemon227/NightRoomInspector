using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunAndShooting : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed;

    public Button closeButton;
    public void Shooting()
    {
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
        yield return new WaitForSeconds(1);
        closeButton.gameObject.SetActive(true);
        rectTransform.anchoredPosition = startPos;
    }
}
