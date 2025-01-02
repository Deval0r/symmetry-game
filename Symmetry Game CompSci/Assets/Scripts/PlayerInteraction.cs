using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject car;
    public float interactionRadius = 3f;
    private bool isInCar = false;
    private CarMovement carMovement;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;

    void Start()
    {
        carMovement = car.GetComponent<CarMovement>();
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
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
        GetComponent<Rigidbody2D>().isKinematic = true; // Disable Rigidbody2D physics
        playerCollider.enabled = false; // Disable player's collision
        playerSprite.enabled = false; // Hide player's sprite
    }

    void ExitCar()
    {
        isInCar = false;
        carMovement.SetPlayerInside(false);
        GetComponent<Rigidbody2D>().isKinematic = false; // Enable Rigidbody2D physics
        playerCollider.enabled = true; // Enable player's collision
        playerSprite.enabled = true; // Show player's sprite
    }
}
