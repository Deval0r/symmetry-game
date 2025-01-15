using UnityEngine;

public class Overlay : MonoBehaviour
{
    private Camera mainCamera;
    public Vector3 offset; // Offset for positioning the UI elements

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object to follow the camera with an offset
        transform.position = mainCamera.transform.position + offset;
    }
}