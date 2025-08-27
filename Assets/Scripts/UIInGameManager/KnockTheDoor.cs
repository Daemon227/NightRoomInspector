using UnityEngine;

public class KnockTheDoor : MonoBehaviour
{
    private Animator animator;

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
    }
    
    public void EndAnimation()
    {
        transform.parent.gameObject.SetActive(false);
        EventManager.OnConversation?.Invoke();
    }

}
