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

            transform.Translate(Vector3.up * moveDirection * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.forward, -turnDirection * turnSpeed * Time.deltaTime);
        }
    }

    public void SetPlayerInside(bool inside)
    {
        isPlayerInside = inside;
    }
}
