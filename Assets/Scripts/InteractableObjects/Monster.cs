using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float delayTime = 5f;

    private void Start()
    {
        StartCoroutine(AttackPlayer());
    }
    public IEnumerator AttackPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 10f, LayerMask.GetMask("Player"));
        if(hit != null)
        {
            
            Debug.Log("Monster is watting to attack the player!");
            yield return new WaitForSeconds(delayTime);
            transform.position = hit.transform.position;
            GameManager.Instance.canMove = false;
            yield return new WaitForSeconds(1f);
            EndingManager.Instance.StartEnding();   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.canMove = false;
        }
    }
}
