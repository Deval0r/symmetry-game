using UnityEngine;

public class LaserPositioning : MonoBehaviour
{
    // Set the desired position on the screen (0, 0 is bottom-left, 1, 1 is top-right)
    public Vector2 screenPosition = new Vector2(0.5f, 0.5f); // Default: center of the screen

    // Optionally, set the camera to use. If null, it will use the main camera.
    public Camera cameraToUse;

    // Optional debug line to visualize the screen position
    private void OnDrawGizmos()
    {
        if (cameraToUse != null)
        {
            Vector3 screenPos = new Vector3(screenPosition.x * Screen.width, screenPosition.y * Screen.height, 0);
            Vector3 worldPos = cameraToUse.ScreenToWorldPoint(screenPos);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldPos, 0.1f); // Draw a red sphere at the world position
        }
    }

    private void Start()
    {
        if (cameraToUse == null)
        {
            cameraToUse = Camera.main;  // Use the main camera if none is set
        }
    }

    void Update()
    {
        // Ensure the object stays in the same screen space position
        // Convert the screen position (screenPosition.x, screenPosition.y) into pixel space
        Vector3 screenPos = new Vector3(screenPosition.x * Screen.width, screenPosition.y * Screen.height, cameraToUse.nearClipPlane);

        // Convert the screen space position into world space
        Vector3 worldPos = cameraToUse.ScreenToWorldPoint(screenPos);

        // Update the object's position
        transform.position = worldPos;

        // Optional: Debugging the result in the console
        Debug.Log("Screen Position: " + screenPosition);
        Debug.Log("World Position: " + worldPos);
    } 

}
