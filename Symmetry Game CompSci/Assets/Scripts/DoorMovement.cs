using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public GameObject doorPrefab;
    public float speed = 1.0f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Debugging doorPrefab assignment
        if (doorPrefab == null)
        {
            Debug.LogError("Door Prefab is not assigned!");
            return;
        }

        // Set the target position to be 5 units above the current position
        targetPosition = doorPrefab.transform.position + new Vector3(0, 5, 0); 
    }

    void Update()
    {
        // Ensure movement is happening if isMoving is true
        if (isMoving && doorPrefab != null)
        {
            // Lerp the position of the doorPrefab towards the target position
            doorPrefab.transform.position = Vector3.Lerp(doorPrefab.transform.position, targetPosition, speed * Time.deltaTime);
            
            // If the door is close enough to the target, stop the movement
            if (Vector3.Distance(doorPrefab.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                Debug.Log("Door reached target position.");
            }
        }
    }

    // Method to start the door movement (linked to the UI button)
    public void StartDoorMovement()
    {
        Debug.Log("StartDoorMovement called");
        isMoving = true;
    }
}
