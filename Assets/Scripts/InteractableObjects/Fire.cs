using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{  
    public float maxY = 11f;
    public float fireSpeed = 5f;
    public GameObject notifficationPanel;
    public Button backButton;
    private Vector2 originFirePos;
    private Vector2 originPlayerPos;

    public GameObject player;
    private void Start()
    {
        originFirePos = gameObject.transform.position;
        originPlayerPos = new Vector2(3, -13);
        backButton.onClick.AddListener(TryAgain);
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
}
