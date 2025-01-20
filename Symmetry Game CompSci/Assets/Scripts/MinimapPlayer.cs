using UnityEngine;

public class MinimapPlayer : MonoBehaviour
{
    // Reference to the UI object to track
    public RectTransform uiObject;

    // Optionally, set the camera to use. If null, it will use the main camera
    public Camera cameraToUse;

    // Z distance from the camera to place the object in world space
    public float zDistance = 10f;

    // Offset to apply to the position
    public Vector3 offset;

    private void Start()
    {
        if (cameraToUse == null)
        {
            cameraToUse = Camera.main;  // Use the main camera if none is set
        }
    }

    void Update()
    {
        if (uiObject != null)
        {
            // Get the screen position of the UI object
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiObject.position);

            // Set the z distance from the camera
            screenPos.z = zDistance;

            // Convert the screen position to world position
            Vector3 worldPos = cameraToUse.ScreenToWorldPoint(screenPos);

            // Update the object's position with offset
            transform.position = worldPos + offset;
        }
    }

    // Optional debug line to visualize the tracking position
    private void OnDrawGizmos()
    {
        if (uiObject != null && cameraToUse != null)
        {
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiObject.position);
            screenPos.z = zDistance;
            Vector3 worldPos = cameraToUse.ScreenToWorldPoint(screenPos) + offset;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(worldPos, 0.1f);  // Draw a red sphere at the adjusted world position
        }
    }
}
