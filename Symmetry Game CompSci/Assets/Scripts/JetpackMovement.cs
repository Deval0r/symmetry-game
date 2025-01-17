using UnityEngine;

public class JetpackController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 5f; // Reduced force for smoother addition
    public float fallMultiplier = 2.5f;
    public float lowJetpackMultiplier = 2f;
    public float maxVerticalSpeed = 10f; // Maximum vertical speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jetpack
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }

        // Cap the maximum vertical speed
        if (rb.linearVelocity.y > maxVerticalSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxVerticalSpeed);
        }

        // Fall faster
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJetpackMultiplier - 1) * Time.deltaTime;
        }
    }
}
