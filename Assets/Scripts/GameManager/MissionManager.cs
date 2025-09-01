using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public TextMeshProUGUI missionText;

    private void Start()
    {
        UpdateMissionUI(0);
    }

    private void OnEnable()
    {
        EventManager.OpenTheDoor += UpdateMissionUI;
        EventManager.OnChangeLanguage += UpdateUIByLanguage;
    }

    private void OnDisable()
    {
        EventManager.OpenTheDoor -= UpdateMissionUI;
        EventManager.OnChangeLanguage -= UpdateUIByLanguage;
    }

    public void UpdateUIByLanguage()
    {
        UpdateMissionUI(0);
    }
    private void UpdateMissionUI(int i)
    {
        var gm = GameManager.Instance;
        int checkedRooms = gm.GetRoomCheckedCount();
        int totalRooms = gm.GetTotalRoomNeedCheck();
        Debug.LogWarning("WTF");
        // Nếu chưa check hết phòng
        if (!gm.checkFullRoom)
        {
            Debug.Log("Check: "+checkedRooms);
            missionText.text = $"{MultiLanguageManager.Instance.GetText("Mission_CheckRoom")} {checkedRooms}/{totalRooms}";
            if (checkedRooms >= totalRooms)
            {
                gm.checkFullRoom = true;
            }
            else
            {
                return;
            }
        }

        // Nếu check hết phòng rồi
        if (gm.reportToBoss)
        {
            missionText.text = MultiLanguageManager.Instance.GetText("Mission_Report");
        }
        else
        {
            missionText.text = MultiLanguageManager.Instance.GetText("Mission_Back");
        }
    }

}
