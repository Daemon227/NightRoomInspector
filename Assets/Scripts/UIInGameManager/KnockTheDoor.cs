using UnityEngine;

public class KnockTheDoor : MonoBehaviour
{
    private Animator animator;
    public AudioSource audioSource;
    public AudioClip knockSound;
    public AudioClip doorOpenSound;
    private void OnEnable()
    {
        EventManager.OpenTheDoor += Knock;

    }
    private void OnDisable()
    {
        EventManager.OpenTheDoor -= Knock;
    }

    public void Knock(int i = 0)
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Open");
        //PlayKnockSound();
    }
    
    public void EndAnimation()
    {
        transform.parent.gameObject.SetActive(false);
        EventManager.OnConversation?.Invoke();
    }

    public void PlayKnockSound()
    {
        audioSource.clip = knockSound;
        audioSource.Play();
    }
    public void PlayDoorOpenSound()
    {
        audioSource.Stop();
        audioSource.clip = doorOpenSound;
        audioSource.Play();
    }
}
