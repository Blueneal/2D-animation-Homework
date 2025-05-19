using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private float jumpForce = 4.0f;
    private float horizontalInput;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform grounded;

    [SerializeField] private bool isRunning;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }

        if(!isGrounded && rb.linearVelocityY <= 0.0f)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            RaycastHit2D hitInfo = Physics2D.Raycast(grounded.position, Vector2.down, .1f);
            if(hitInfo.collider != null)
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    isGrounded = true;
                    animator.SetBool("isFalling", false);
                }
            }
        }
    }
}
