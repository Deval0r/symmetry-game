using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject car;
    public float interactionRadius = 3f;
    private bool isInCar = false;
    private CarMovement carMovement;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Rigidbody2D rb;

    void Start()
    {
        carMovement = car.GetComponent<CarMovement>();
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInCar)
            {
                ExitCar();
            }
            else if (Vector2.Distance(transform.position, car.transform.position) <= interactionRadius)
            {
                EnterCar();
            }
        }

        if (isInCar)
        {
            // Update player's local position relative to the car
            transform.position = car.transform.position + (Vector3)(car.transform.position - transform.position).normalized * 0.5f;
        }
    }

    void EnterCar()
    {
        isInCar = true;
        carMovement.SetPlayerInside(true);
        rb.bodyType = RigidbodyType2D.Kinematic; // Disable Rigidbody2D physics
        playerCollider.enabled = false; // Disable player's collision
        playerSprite.enabled = false; // Hide player's sprite
    }

    void ExitCar()
    {
        isInCar = false;
        carMovement.SetPlayerInside(false);
        rb.bodyType = RigidbodyType2D.Dynamic; // Enable Rigidbody2D physics
        playerCollider.enabled = true; // Enable player's collision
        playerSprite.enabled = true; // Show player's sprite
    }
}
