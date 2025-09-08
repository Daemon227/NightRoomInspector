using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{  
    public float maxY = 11f;
    public float fireSpeed = 5f;
    public GameObject notifficationPanel;
    public Button backButton;
    public TextMeshProUGUI title;
    private Vector2 originFirePos;
    private Vector2 originPlayerPos;

    public GameObject player;
    
    public AudioSource audioSource;
    public AudioClip fireSound;
    private void Start()
    {
        originFirePos = gameObject.transform.position;
        originPlayerPos = new Vector2(3, -13);
        backButton.onClick.AddListener(TryAgain);
        PlayFireSound();
    }
    private void Update()
    {
        if (EndingManager.Instance.canFireMove)
        {
            FireMove();
        }          
    }
    public void FireMove()
    {
        gameObject.transform.Translate(Vector2.up * fireSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.canMove = false;
            EndingManager.Instance.canFireMove = false;
            notifficationPanel.SetActive(true);
            UpdateUI();
        }
    }

    public void TryAgain()
    {
        notifficationPanel.SetActive(false);
        player.transform.position = originPlayerPos;
        gameObject.transform.position = originFirePos;
        EndingManager.Instance.canFireMove = true;
        GameManager.Instance.canMove = true;
    }

    public void PlayFireSound()
    {
        audioSource.clip = fireSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void UpdateUI()
    {
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = MultiLanguageManager.Instance.GetText("Button_Try_Again");
        title.text = MultiLanguageManager.Instance.GetText("You_Died");
    }
}
