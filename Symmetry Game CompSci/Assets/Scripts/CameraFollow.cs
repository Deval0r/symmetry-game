using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Default offset
    public float offsetMultiplier = 1f; // Multiplier for offset adjustment
    public float zoomOutFactor = 0.1f; // Factor by which the camera zooms out based on speed
    public float maxZoomOut = 10f; // Maximum orthographic size for the camera
    public float minZoomIn = 5f; // Minimum orthographic size for the camera

    private Vector3 previousPlayerPosition;

    void Start()
    {
        previousPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        // Determine movement direction
        Vector3 playerMovement = player.position - previousPlayerPosition;
        previousPlayerPosition = player.position;

        // Adjust offset based on movement direction
        if (playerMovement.x > 0)
        {
            // Moving right
            offset = new Vector3(Mathf.Abs(offset.x) * offsetMultiplier, offset.y, offset.z);
        }
        else if (playerMovement.x < 0)
        {
            // Moving left
            offset = new Vector3(-Mathf.Abs(offset.x) * offsetMultiplier, offset.y, offset.z);
        }

        // Target position based on the player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Additional offset to the right
        Vector3 rightOffset = new Vector3(9f, 0, 0); // Adjust the value as needed
        desiredPosition += rightOffset;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ensure the camera's z-position stays at -10
        smoothedPosition.z = -10;

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Calculate player speed
        float playerSpeed = playerMovement.magnitude / Time.deltaTime;

        // Adjust camera's orthographic size based on player speed
        float targetZoom = Mathf.Clamp(minZoomIn + playerSpeed * zoomOutFactor, minZoomIn, maxZoomOut);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, smoothSpeed);
    }
}