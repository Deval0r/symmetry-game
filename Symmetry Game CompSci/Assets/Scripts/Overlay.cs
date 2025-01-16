using UnityEngine;

public class Overlay : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialScale;
    public Vector3 offset; // Offset for positioning the UI elements

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
        // Store the initial scale of the object
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the offset in world space relative to the camera's position
        Vector3 worldOffset = mainCamera.transform.TransformDirection(offset);

        // Move the object to follow the camera with the world-space offset
        Vector3 newPosition = mainCamera.transform.position + worldOffset;
        newPosition.z = 0; // Ensure the z position remains 0 (for 2D view)
        transform.position = newPosition;

        // Adjust the scale of the object to remain visually consistent
        float scaleFactor = mainCamera.orthographicSize / 5f; // Adjust the denominator as needed
        transform.localScale = initialScale * scaleFactor;
    }
}
