using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject car;
    public float interactionRadius = 3f;
    private bool isInCar = false;
    private CarMovement carMovement;

    void Start()
    {
        carMovement = car.GetComponent<CarMovement>();
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
            // Set the player's local position to the car's position
            transform.position = car.transform.TransformPoint(Vector3.zero);
        }
    }

    void EnterCar()
    {
        isInCar = true;
        carMovement.SetPlayerInside(true);
        GetComponent<Rigidbody2D>().isKinematic = true; // Disable Rigidbody2D physics
        transform.SetParent(car.transform); // Parent player to car
    }

    void ExitCar()
    {
        isInCar = false;
        carMovement.SetPlayerInside(false);
        GetComponent<Rigidbody2D>().isKinematic = false; // Enable Rigidbody2D physics
        transform.SetParent(null); // Unparent player from car
    }
}
