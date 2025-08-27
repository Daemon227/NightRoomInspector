using UnityEngine;
using UnityEngine.EventSystems;

public class ScanEffect : MonoBehaviour
{
    void Update()
    {
        Scan();
    }

    public void Scan()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}