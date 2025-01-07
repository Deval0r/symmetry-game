using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Offset between the camera and the player

    void LateUpdate()
    {
        // Target position based on the player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ensure the camera's z-position stays at -10
        smoothedPosition.z = -10;

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
