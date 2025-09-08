using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotePanel : MonoBehaviour, IMenu
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI firstMission;
    public TextMeshProUGUI secondMission;
    public TextMeshProUGUI thirdMission;
    public TextMeshProUGUI note;

    public void UpdateUI()
    {
        title.text = MultiLanguageManager.Instance.GetText("Note_Title");
        firstMission.text = MultiLanguageManager.Instance.GetText("Note_First_Mission");
        secondMission.text = MultiLanguageManager.Instance.GetText("Note_Second_Mission");
        thirdMission.text = MultiLanguageManager.Instance.GetText("Note_Third_Mission");
        note.text = MultiLanguageManager.Instance.GetText("Note_Mission");
    }

    public void ActiveEvent()
    {
        UpdateUI();
    }
}
