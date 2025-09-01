using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject trash;
    public GameObject scanMachine;
    public GameObject gunObj;

    private void OnEnable()
    {
        EventManager.OnChangeDay += SpawnObjectByDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= SpawnObjectByDay;
    }
    private void Start()
    {
        SpawnObjectByDay();
    }
    public void SpawnObjectByDay()
    {
        GameObject room = GameManager.Instance.reportedRoom;
        if ( room != null && !room.GetComponent<Door>().isMonster)
        {
            trash.SetActive(true);
        }   
        
        if(GameManager.Instance.currentDay == 3 && !GameManager.Instance.canScan)
        {
            scanMachine.SetActive(true);
        }

        if (GameManager.Instance.currentDay == 4 && !GameManager.Instance.canShoot)
        {
            gunObj.SetActive(true);
        }
    }
}
