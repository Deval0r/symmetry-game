using UnityEngine;

public class Overlay : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialScale;

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
        // Move the object to the center of the camera's viewport
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane);
        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(screenCenter);
        transform.position = worldPosition;

        // Adjust the scale of the object to remain visually consistent
        float scaleFactor = mainCamera.orthographicSize / 5f; // Adjust the denominator as needed
        transform.localScale = initialScale * scaleFactor;
    }
}