using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject trash;
    public GameObject HidenNpc;
    public GameObject ObjectEding1;

    public void SpawnObjectByDay()
    {
        GameObject room = GameManager.Instance.reportedRoom;
        if ( room != null && !room.GetComponent<Door>().isMonster)
        {
            trash.SetActive(true);
        }       
    }
}
