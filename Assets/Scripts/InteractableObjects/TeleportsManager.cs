using System.Collections;
using UnityEngine;

public class TeleportsManager : MonoBehaviour
{
    public Transform otherPort;
    public GameObject loadingPanel;

    public IEnumerator PlayLoading()
    {
        GameManager.Instance.canMove = false;
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadingPanel.SetActive(false);
        GameManager.Instance.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent.position = otherPort.position;
            StartCoroutine(PlayLoading());
        }
    }
}
