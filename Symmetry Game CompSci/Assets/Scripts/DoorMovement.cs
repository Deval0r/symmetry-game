using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private bool isOpening = false;
    private bool isClosing = false;
    private Vector3 targetPosition;
    private Vector3 closePosition;
    private float speed = 2.0f;

    void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        closePosition = transform.position;
        Debug.Log("Door initialized. Close position: " + closePosition + ", Open position: " + targetPosition);
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            Debug.Log("Opening door. Current position: " + transform.position);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isOpening = false;
                Debug.Log("Door fully opened.");
            }
        }
        if (isClosing)
        {
            transform.position = Vector3.Lerp(transform.position, closePosition, Time.deltaTime * speed);
            Debug.Log("Closing door. Current position: " + transform.position);
            if (Vector3.Distance(transform.position, closePosition) < 0.01f)
            {
                transform.position = closePosition;
                isClosing = false;
                Debug.Log("Door fully closed.");
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        isClosing = false;
        Debug.Log("OpenDoor called.");
    }

    public void CloseDoor()
    {
        isClosing = true;
        isOpening = false;
        Debug.Log("CloseDoor called.");
    }
}