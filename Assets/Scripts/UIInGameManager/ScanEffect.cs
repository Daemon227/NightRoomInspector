using TMPro;
using UnityEngine;

public class ScanSystem : MonoBehaviour
{
    public RectTransform scanCircle;  // Vòng quét
    public float scanRadius = 100f;   // Bán kính vòng quét
    public RectTransform brainPoint;
    public RectTransform heartPoint;

    public GameObject heart;
    public GameObject heartRot;
    public GameObject brain;
    public GameObject brainRot;

    public bool[] scanShowDay3;// brain, heart;
    public bool[] scanShowDay4;// brain, heart;

    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        MoveScanUI();
        CheckScan();
    }

    public void MoveScanUI()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        scanCircle.position = mousePos;
    }

    public void CheckScan()
    {
        float distanceToBrain = Vector2.Distance(scanCircle.position, brainPoint.position);
        float distanceToHeart = Vector2.Distance(scanCircle.position, heartPoint.position);

        // Nếu không nằm trong bán kính thì tắt hết
        if (distanceToBrain > scanRadius)
        {
            brain.SetActive(false);
            brainRot.SetActive(false);

        }
        if (distanceToHeart > scanRadius)
        {
            heart.SetActive(false);
            heartRot.SetActive(false);
        }

        if (distanceToBrain <= scanRadius || distanceToHeart <= scanRadius)
        {
            // Xác định target gần hơn
            bool isBrainCloser = distanceToBrain <= distanceToHeart;

            // Lấy index: 0 = brain, 1 = heart
            int index = isBrainCloser ? 0 : 1;

            // Chọn đúng object để hiển thị
            GameObject normalObj = isBrainCloser ? brain : heart;
            GameObject rotObj = isBrainCloser ? brainRot : heartRot;
            GameObject otherNormalObj = isBrainCloser ? heart : brain;
            GameObject otherRotObj = isBrainCloser ? heartRot : brainRot;

            // Lấy trạng thái từ dữ liệu theo ngày
            bool shouldShow = false;
            if (GameManager.Instance.currentDay == 3)
                shouldShow = scanShowDay3[index];
            else if (GameManager.Instance.currentDay >= 4)
                shouldShow = scanShowDay4[index];

            // Bật/tắt theo kết quả
            SetActivePair(normalObj, rotObj, shouldShow);
            otherNormalObj.SetActive(false);
            otherRotObj.SetActive(false);
        }
    }

    private void SetActivePair(GameObject normal, GameObject rotated, bool showNormal)
    {
        normal.SetActive(showNormal);
        rotated.SetActive(!showNormal);
    }

    public void UpdateUI()
    {
        textMeshProUGUI.text = MultiLanguageManager.Instance.GetText("Instruction_Scan");
    }
}
