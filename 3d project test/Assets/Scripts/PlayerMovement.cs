using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Player movement
        float moveForwardBackward = Input.GetAxis("Vertical") * speed * Time.deltaTime; // W/S keys
        float moveLeftRight = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // A/D keys

        Vector3 move = playerBody.right * moveLeftRight + playerBody.forward * moveForwardBackward;
        move.y = 0; // Ensure the player stays on the ground
        playerBody.Translate(move, Space.World);
    }
}