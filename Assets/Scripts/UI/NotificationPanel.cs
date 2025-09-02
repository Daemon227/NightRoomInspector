using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class NotificationPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public TextMeshProUGUI instruction;

    public GameObject menu;
    private void Start()
    {
        StartCoroutine(ShowNotiffication());
    }
    public IEnumerator ShowNotiffication()
    {
        title.text = MultiLanguageManager.Instance.GetText("Menu_Warning");
        content.text = MultiLanguageManager.Instance.GetText("Warning");
        instruction.text = MultiLanguageManager.Instance.GetText("Instruction_Click_To_Continue");
        yield return new WaitUntil(()=>Input.GetMouseButtonUp(0));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        menu.SetActive(true);
        AudioManager.Instance.PlayThemeMusic();
        this.gameObject.SetActive(false);
    }
}
