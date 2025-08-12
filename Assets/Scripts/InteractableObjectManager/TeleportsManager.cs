using System.Collections;
using UnityEngine;

public class TeleportsManager : MonoBehaviour
{
    public Transform otherPort;
    public GameObject loadingPanel;

    public IEnumerator PlayLoading()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadingPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = otherPort.position;
            StartCoroutine(PlayLoading());
        }
    }
}
