using UnityEngine;

public class WorldToScreenExample : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Vector3 worldPosition = new Vector3(-14.67f, -1.26f, 0f); // The world position to convert

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Use the main camera if not set
        }

        // Convert world position to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        // Log the result
        Debug.Log("Screen Coordinates:" + screenPosition);
    }
}
