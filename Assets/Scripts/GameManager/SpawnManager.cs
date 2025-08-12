using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject trash;
    public GameObject HidenNpc;

    private void OnEnable()
    {
        EventManager.OnChangeDay += SpawnObjectByDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= SpawnObjectByDay;
    }

    public void SpawnObjectByDay()
    {
        int currentday = GameManager.Instance.currentDay;
        if (GameManager.Instance.reportedSomeone)
        {
            trash.SetActive(true);
            GameManager.Instance.reportedSomeone = false;
        }
        if (currentday == 3)
        {
            HidenNpc.SetActive(true);
        }
    }
}
