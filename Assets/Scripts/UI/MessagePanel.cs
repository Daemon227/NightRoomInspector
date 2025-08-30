using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
    }
    public void SetUIContent(int index)//0 is scanMachine, 1 is gun
    {
        title.text = MultiLanguageManager.Instance.GetText("Message");
        string scanText = "";
        string gunText = "";
        if (MultiLanguageManager.Instance.currentLanguage.Equals("vn"))
        {
            scanText = "Có vẻ các vị khách thuê phòng của chúng ta đã thay đổi khá nhiều, vì thé việc xác định kẻ nào bất thường với cậu bây giờ khá khó, tôi gửi cho cậu Chiếc máy quét này để cậu có thể sử dụng nó để kiểm tra các bộ phận bên trong của họ, nếu có gì bất thường hãy báo lại cho tôi.";
            gunText = "Mọi chuyện ngày càng khó khăn hơn vì thế tôi gửi tặng món quà này, hãy sử dụng nó khi thấy kẻ nào đó không phải con người. Đừng do dự hãy sử dụng nó";
        }
        if (MultiLanguageManager.Instance.currentLanguage.Equals("en"))
        {
            scanText = "It seems that our tenants have changed quite a lot, so it’s now rather difficult for you to identify who is unusual. I’m sending you this scanner so you can use it to examine their internal parts. If you find anything abnormal, report it to me.";
            gunText = "Things are getting more difficult, so I’m giving you this gift. Use it whenever you see someone who is not human. Don’t hesitate—just use it.";
        }
        
        if(index == 0)
        {
            content.text = scanText;
        }
        else
        {
            content.text = gunText;
        }
    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.canMove = true;
    }


}
