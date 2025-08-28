using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Window : InteractableObject
{
    public GameObject windowPanel;
    public GameObject window;
    public RectTransform posToMove;
    public float speed = 1000f;

    public Button backButton;
    public Button jumpButton;

    private Vector3 originPos;
    private RectTransform rectTranform;

    private void Start()
    {
        rectTranform = window.GetComponent<RectTransform>();
        originPos = rectTranform.anchoredPosition;
        backButton.onClick.AddListener(() => StartCoroutine(CloseWindow()));
        jumpButton.onClick.AddListener(JumpOut);
    }
    public override void SetButtonLanguage(TextMeshProUGUI text, int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                text.text = MultiLanguageManager.Instance.GetText("Open_Window");
                break;
            case 1:
                text.text = MultiLanguageManager.Instance.GetText("Leave");
                break;
        }
    }
    public override void HandleOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0:
                OpenWindow();
                SetLanguage();
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
        if(EndingManager.Instance.endingID == 1)
        {
            jumpButton.gameObject.SetActive(true);    
        }
        StartCoroutine(MoveWindow(rectTranform, posToMove.anchoredPosition));
        
    }
    IEnumerator MoveWindow(RectTransform rt, Vector3 endPos)
    {
        EndingManager.Instance.canFireMove = false;
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
        EndingManager.Instance.canFireMove = true;
    }

    public void JumpOut()
    {
        // cho nhay ra ngoai
        Debug.Log("Jump out the window");
        GameManager.Instance.canMove = false;
        EndingManager.Instance.canFireMove = false;
        windowPanel.SetActive(false);
        EndingManager.Instance.StartEnding();
    }

    public void SetLanguage()
    {
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Back");
        jumpButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Jump");
    }
}
