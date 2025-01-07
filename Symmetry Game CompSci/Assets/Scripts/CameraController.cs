using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TopMovement topMovementScript;
    public CarMovement carMovementScript;
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Offset between the camera and the player/car

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topMovementScript = FindObjectOfType<TopMovement>();
        carMovementScript = FindObjectOfType<CarMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition;

        if (carMovementScript.isPlayerInside)
        {
            targetPosition = carMovementScript.transform.position + offset;
        }
        else
        {
            targetPosition = topMovementScript.transform.position + offset;
        }

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Ensure the camera's z-position stays at -10
        smoothedPosition.z = -10;

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
