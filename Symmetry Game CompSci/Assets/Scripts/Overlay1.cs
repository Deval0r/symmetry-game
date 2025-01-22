using UnityEngine;

public class Overlay1 : MonoBehaviour
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


        // Adjust the scale of the object to remain visually consistent
        float scaleFactor = mainCamera.orthographicSize / 5f; // Adjust the denominator as needed
        transform.localScale = initialScale * scaleFactor;
    }
}
