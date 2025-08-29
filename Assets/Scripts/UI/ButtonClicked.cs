using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySFXButtonClicked();
    }
}
