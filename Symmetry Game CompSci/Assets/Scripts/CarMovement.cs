using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public bool isPlayerInside = false;

    void Update()
    {
        if (isPlayerInside)
        {
            float moveDirection = Input.GetAxis("Vertical");
            float turnDirection = Input.GetAxis("Horizontal");

            // Adjust move speed based on direction
            float currentMoveSpeed = moveDirection < 0 ? moveSpeed * 0.75f : moveSpeed;

            if (moveDirection != 0) // Only rotate when there is forward or backward movement
            {
                transform.Translate(Vector3.up * moveDirection * currentMoveSpeed * Time.deltaTime);
                transform.Rotate(Vector3.forward, -turnDirection * turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.up * moveDirection * currentMoveSpeed * Time.deltaTime); // Move forward/backward even when not rotating
            }
        }
    }

    public void SetPlayerInside(bool inside)
    {
        isPlayerInside = inside;
    }
}
