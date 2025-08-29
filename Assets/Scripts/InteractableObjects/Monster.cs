using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float delayTime = 5f;

    public AudioSource monsterAudioSource;
    public AudioClip walkSound;
    public AudioClip monsterSound;
    private void Start()
    {
        StartCoroutine(AttackPlayer());
    }
    public IEnumerator AttackPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 10f, LayerMask.GetMask("Player"));
        if(hit != null)
        {
            monsterAudioSource.clip = walkSound;
            monsterAudioSource.Play();
            monsterAudioSource.loop = true;
            Debug.Log("Monster is watting to attack the player!");
            yield return new WaitForSeconds(delayTime);
            PlayMonsterSound();
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

    public void PlayMonsterSound()
    {
        monsterAudioSource.Stop();
        monsterAudioSource.clip = monsterSound;
        monsterAudioSource.Play();
        monsterAudioSource.loop = false;
    }
}
