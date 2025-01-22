using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public GameObject Door;
    public float speed = 5.0f; // Increased speed for faster movement
    public Vector3 movementAmount = new Vector3(0, 5, 0); // Editable in the inspector
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isOpen = false;

    void Start()
    {
        // Set the initial positions
        initialPosition = Door.transform.position;
        targetPosition = initialPosition + movementAmount;
        Debug.Log("DoorMovement initialized. Initial position: " + initialPosition + ", Target position: " + targetPosition);
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 target = isOpen ? initialPosition : targetPosition;
            Door.transform.position = Vector3.Lerp(Door.transform.position, target, speed * Time.deltaTime);
            Debug.Log("Door moving. Current position: " + Door.transform.position);
            if (Vector3.Distance(Door.transform.position, target) < 0.5f)
            {
                //Door.transform.position = target; // Snap to target position
                isMoving = false;
                isOpen = !isOpen;
                Debug.Log("Door reached the target position. Stopping movement.");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDoor();
        }
    }

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            isMoving = true;
            Debug.Log("ToggleDoor called. Starting to move.");
        }
    }
}