using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private bool isGrounded;

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
        Debug.Log("Is Grounded: " + isGrounded);

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("Jumping");
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
        // Cast three raycasts: left, right, and center
        Vector2 position = transform.position;
        Vector2 leftRayOrigin = position + Vector2.left * 0.7f;
        Vector2 rightRayOrigin = position + Vector2.right * 0.7f;
        Vector2 centerRayOrigin = position;

        bool isLeftGrounded = Physics2D.Raycast(leftRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        bool isRightGrounded = Physics2D.Raycast(rightRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        bool isCenterGrounded = Physics2D.Raycast(centerRayOrigin, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(leftRayOrigin, Vector2.down * 0.75f, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * 0.75f, Color.red);
        Debug.DrawRay(centerRayOrigin, Vector2.down * 0.75f, Color.red);

        return isLeftGrounded || isRightGrounded || isCenterGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.75f); // Ground check center
        Gizmos.DrawLine(transform.position + Vector3.left * 0.7f, transform.position + Vector3.left * 0.7f + Vector3.down * 0.75f); // Ground check left
        Gizmos.DrawLine(transform.position + Vector3.right * 0.7f, transform.position + Vector3.right * 0.7f + Vector3.down * 0.75f); // Ground check right
    }
}
