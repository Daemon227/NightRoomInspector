using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window : InteractableObject
{
    public GameObject windowPanel;
    public GameObject window;
    public RectTransform posToMove;
    public float speed = 1000f;

    public Button button;

    private Vector3 originPos;
    private RectTransform rectTranform;

    private bool hasWindowOpened = false;

    private void Start()
    {
        rectTranform = window.GetComponent<RectTransform>();
        originPos = rectTranform.anchoredPosition;
        button.onClick.AddListener(() => StartCoroutine(CloseWindow()));
    }

    private void OnEnable()
    {
        EventManager.OnChangeDay += ChangeDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= ChangeDay;
    }

    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                OpenWindow();
                ClearOption();
                break;
            case 1:
                ClearOption();
                // set di chuyen
                GameManager.Instance.canMove = true;
                break;
        }
    }

    public void OpenWindow()
    {
        windowPanel.SetActive(true);
        StartCoroutine(MoveWindow(rectTranform, posToMove.anchoredPosition));
        
        if (!hasWindowOpened)
        {
            hasWindowOpened = true;
            EndingManager.Instance.lookOutSideIndex += 1;
            if(EndingManager.Instance.lookOutSideIndex == 3)
            {
                Debug.Log("See Police");
            }
        }
    }
    IEnumerator MoveWindow(RectTransform rt, Vector3 endPos)
    {
        while (Vector2.Distance(rt.anchoredPosition, endPos) > 0.01f)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition, endPos, speed * Time.deltaTime
            );
            yield return null; // đợi tới frame tiếp theo
        }

        // Đảm bảo chính xác vị trí đích
        rt.anchoredPosition = endPos;
    }

    public IEnumerator CloseWindow()
    {
        yield return MoveWindow(rectTranform, originPos);
        yield return new WaitForSeconds(1);
        windowPanel.SetActive(false);

        // set di chuyen
        GameManager.Instance.canMove = true;
    }

    public void ChangeDay()
    {
        hasWindowOpened = false;
    }
}
