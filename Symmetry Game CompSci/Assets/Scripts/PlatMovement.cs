using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWall;
    private float wallJumpCooldown = 0.1f;
    private float lastWallJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Ground check using three raycasts for left, center, and right
        isGrounded = CheckGrounded();
        isWall = WallCheck();

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Wall jump with continuous activation
        if (Input.GetKey(KeyCode.W) && isWall && Time.time > lastWallJumpTime + wallJumpCooldown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            lastWallJumpTime = Time.time;
        }

        // Fall faster
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool CheckGrounded()
    {
        // Cast three raycasts: left, right, and center from the foot level, slightly lower again
        Vector2 position = transform.position;
        Vector2 leftRayOrigin = position + Vector2.left * 0.7f + Vector2.down * 0.6f;
        Vector2 rightRayOrigin = position + Vector2.right * 0.7f + Vector2.down * 0.6f;
        Vector2 centerRayOrigin = position + Vector2.down * 0.6f;

        bool isLeftGrounded = Physics2D.Raycast(leftRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        bool isRightGrounded = Physics2D.Raycast(rightRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        bool isCenterGrounded = Physics2D.Raycast(centerRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(leftRayOrigin, Vector2.down * 0.75f, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * 0.75f, Color.red);
        Debug.DrawRay(centerRayOrigin, Vector2.down * 0.75f, Color.red);

        return isLeftGrounded || isRightGrounded || isCenterGrounded;
    }

    private bool WallCheck()
    {
        // Cast two raycasts from the foot level, slightly lower again
        Vector2 position = transform.position;
        Vector2 leftRayOrigin = position + Vector2.left * 0.7f + Vector2.down * 0.7f;
        Vector2 rightRayOrigin = position + Vector2.right * 0.7f + Vector2.down * 0.7f;

        bool isLeftWall = Physics2D.Raycast(leftRayOrigin, Vector2.left, 0.75f, LayerMask.GetMask("Ground"));
        bool isRightWall = Physics2D.Raycast(rightRayOrigin, Vector2.right, 0.75f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(leftRayOrigin, Vector2.left * 0.75f, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.right * 0.75f, Color.red);

        return isLeftWall || isRightWall;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.down * 0.6f, transform.position + Vector3.down * 1.35f); // Ground check center from foot level, slightly lower
        Gizmos.DrawLine(transform.position + Vector3.left * 0.7f + Vector3.down * 0.6f, transform.position + Vector3.left * 0.7f + Vector3.down * 1.35f); // Ground check left from foot level, slightly lower
        Gizmos.DrawLine(transform.position + Vector3.right * 0.7f + Vector3.down * 0.6f, transform.position + Vector3.right * 0.7f + Vector3.down * 1.35f); // Ground check right from foot level, slightly lower

        // Sideways wall check rays from foot level, slightly lower
        Gizmos.DrawLine(transform.position + Vector3.left * 0.7f + Vector3.down * 0.7f, transform.position + Vector3.left * 1.45f + Vector3.down * 0.6f); // Wall check left from foot level, slightly lower
        Gizmos.DrawLine(transform.position + Vector3.right * 0.7f + Vector3.down * 0.7f, transform.position + Vector3.right * 1.45f + Vector3.down * 0.6f); // Wall check right from foot level, slightly lower
    }
}
