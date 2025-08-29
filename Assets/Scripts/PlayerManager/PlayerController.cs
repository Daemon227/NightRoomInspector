using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    public Transform spawnPos;
    public GameObject targetPoint;
    Animator playerAnim;

    private string lastMoveAxis = "Horizontal"; // Mặc định ưu tiên ngang

    public AudioSource audioSource;
    public AudioClip clip;

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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Cập nhật hướng bấm cuối cùng
        if (Mathf.Abs(x) > 0) lastMoveAxis = "Horizontal";
        if (Mathf.Abs(y) > 0) lastMoveAxis = "Vertical";

        // Nếu cả hai phím được bấm → ưu tiên hướng cuối cùng
        if (x != 0 && y != 0)
        {
            if (lastMoveAxis == "Horizontal") y = 0;
            else x = 0;
        }

        Vector3 moveVector = new Vector3(x, y, 0);

        if (moveVector == Vector3.zero || !GameManager.Instance.canMove)
        {
            playerAnim.SetBool("IsMoving", false);
            audioSource.loop = false;
            audioSource.Stop();
            return;
        }

        playerAnim.SetBool("IsMoving", true);
        playerAnim.SetFloat("xValue", x);
        playerAnim.SetFloat("yValue", y);
        transform.Translate(moveVector * speed * Time.deltaTime);
        // Xử lý âm thanh bước chân
        if (!audioSource.isPlaying) // chỉ play nếu chưa phát
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (moveVector != Vector3.zero)
        {
            targetPoint.transform.position = moveVector.normalized + transform.position;        
        }
    }

    public void ChangePosByDay()
    {
        transform.position = spawnPos.position;
    }
}
