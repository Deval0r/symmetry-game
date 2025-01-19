using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isWall;
    private float wallJumpCooldown = 0.1f;
    private float lastWallJumpTime;

    public Vector3 UICoordiantes;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isIdleLeft", false);
        }
        else if (moveInput < 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isIdleLeft", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingLeft", false);
            if (animator.GetBool("isWalkingLeft"))
            {
                animator.SetBool("isIdleLeft", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isIdleLeft", false);
            }
        }

        // Handle jumping and other movement logic here
    }
}