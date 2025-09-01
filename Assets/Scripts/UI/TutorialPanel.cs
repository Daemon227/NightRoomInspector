using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public CutSceneData tutorialData;
    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI nextText;

    public void ActiveEvent()
    {        
        StartCoroutine(ShowTutorial());
    }
    public IEnumerator ShowTutorial()
    {
        Time.timeScale = 0;
        title.text = MultiLanguageManager.Instance.GetText("Menu_Tutorial");
        for (int i = 0; i < tutorialData.enDescriptions.Length; i++)
        {
            image.sprite = tutorialData.images[i];
            instructionText.text = "";
            if (MultiLanguageManager.Instance.currentLanguage.Equals("vn"))
            {
                instructionText.text = tutorialData.vnDescriptions[i];
            }
            else instructionText.text = tutorialData.enDescriptions[i];

            nextText.text = MultiLanguageManager.Instance.GetText("Menu_Instruction_ClickToContinue");
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
