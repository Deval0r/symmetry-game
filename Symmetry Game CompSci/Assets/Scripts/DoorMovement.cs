using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public GameObject Door;
    public float speed = 1.0f;
    private Vector3 targetPositionUp;
    private Vector3 targetPositionDown;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isAtTop = false;

    void Start()
    {
        // Set the initial positions
        targetPositionUp = Door.transform.position + new Vector3(0, 5, 0);
        targetPositionDown = Door.transform.position;
        Debug.Log("DoorMovement initialized. Target positions set.");
    }

    void Update()
    {
        if (isMovingUp)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, targetPositionUp, speed * Time.deltaTime);
            Debug.Log("Door moving up. Current position: " + Door.transform.position);
            if (Vector3.Distance(Door.transform.position, targetPositionUp) < 0.01f)
            {
                Door.transform.position = targetPositionUp; // Snap to target position
                isMovingUp = false;
                isAtTop = true;
                Debug.Log("Door reached the upper position. Stopping movement.");
            }
        }
        else if (isMovingDown)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, targetPositionDown, speed * Time.deltaTime);
            Debug.Log("Door moving down. Current position: " + Door.transform.position);
            if (Vector3.Distance(Door.transform.position, targetPositionDown) < 0.01f)
            {
                Door.transform.position = targetPositionDown; // Snap to target position
                isMovingDown = false;
                isAtTop = false;
                Debug.Log("Door reached the lower position. Stopping movement.");
            }
        }
    }

    // Method to toggle the door movement (linked to the UI button)
    public void StartDoorMovement()
    {
        if (!isMovingUp && !isMovingDown)
        {
            if (isAtTop)
            {
                isMovingDown = true;
                Debug.Log("Door movement started. Moving down.");
            }
            else
            {
                isMovingUp = true;
                Debug.Log("Door movement started. Moving up.");
            }
        }
    }
}
