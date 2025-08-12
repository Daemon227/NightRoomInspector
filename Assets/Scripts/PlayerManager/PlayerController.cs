using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    public Transform spawnPos;
    Animator playerAnim;

    private void OnEnable()
    {
        EventManager.OnChangeDay += ChangePosByDay;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDay -= ChangePosByDay;
    }
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(x, y,0);
        if (moveVector == Vector3.zero || !GameManager.Instance.canMove)
        {
            playerAnim.SetBool("IsMoving", false);
            return;
        }
        playerAnim.SetBool("IsMoving", true);
        playerAnim.SetFloat("xValue", x);
        playerAnim.SetFloat("yValue", y);
        transform.Translate(moveVector * speed * Time.deltaTime);
    }

    public void ChangePosByDay()
    {
        transform.position = spawnPos.position;
    }
}
