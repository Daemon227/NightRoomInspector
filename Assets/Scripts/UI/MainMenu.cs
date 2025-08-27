using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public List<Button> menuButtons;
    public List<GameObject> panels;

    public GameObject loadingPanel;
    public GameObject cutScenePanel;
    public CutSceneData cutSceneData;
    public Image image;
    public TextMeshProUGUI descriptionText;

    private void OnEnable()
    {
        EventManager.OnChangeLanguage += SettupButtonLanguage;
    }
    private void OnDisable()
    {
        EventManager.OnChangeLanguage -= SettupButtonLanguage;
    }
    public void Start()
    {
        SettupButtonLanguage();
        menuButtons[0].onClick.AddListener(() => StartCoroutine(PlayNewGame()));
        for (int i = 1; i < menuButtons.Count; i++)
        {
            int index = i-1; // Capture the current value of i
            menuButtons[i].onClick.AddListener(() => OpenPanel(index));
        }
    }
    public void OpenPanel(int i)
    {
        panels[i].SetActive(true);
        panels[i].GetComponent<IMenu>().ActiveEvent();
        menuPanel.SetActive(false);
    }

    public IEnumerator PlayNewGame()
    {
        loadingPanel.SetActive(true);
        DataLoading.Instance.currentGameData = null;
        yield return new WaitForSeconds(1f);
        loadingPanel.SetActive(false);
        cutScenePanel.SetActive(true);

        for (int i = 0; i < cutSceneData.descriptions.Length; i++)
        {
            image.sprite = cutSceneData.images[i];
            descriptionText.text = "";
            descriptionText.text = cutSceneData.descriptions[i];
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        cutScenePanel.SetActive(false);
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void SettupButtonLanguage()
    {
        menuButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Menu_New_Game");
        menuButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Menu_Continue");
        menuButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Menu_Setting");
        menuButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Menu_About");
    }
}

public interface IMenu
{
    public void ActiveEvent();
}
