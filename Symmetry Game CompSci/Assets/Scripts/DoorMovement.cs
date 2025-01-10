using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private bool isOpening = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.001f);
            if (transform.position == targetPosition)
            {
                isOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}
