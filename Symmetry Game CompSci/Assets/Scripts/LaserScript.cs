using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float rotationSpeed;
    public float maxDistance;
    public LayerMask mapGroundLayer;
    public LayerMask playerDummyLayer;
    public LayerMask mapMirrorLayer;
    public LineRenderer lineRenderer;
    private bool playerHit;

    void Update()
    {
        // Rotate the prefab clockwise
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);

        // Direction of the ray based on the prefab's rotation
        Vector3 direction = transform.right;

        // Cast the ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, mapGroundLayer | playerDummyLayer | mapMirrorLayer);

        // If the ray hits an object
        if (hit.collider != null)
        {
            // Set the positions of the LineRenderer to draw the ray
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            // Check if the hit object is in the player dummy layer
            if (((1 << hit.collider.gameObject.layer) & playerDummyLayer) != 0)
            {
                playerHit = true;
                Debug.Log("PlayerDummy is touching the laser.");
            }
            // Check if the hit object is in the map mirror layer
            else if (((1 << hit.collider.gameObject.layer) & mapMirrorLayer) != 0)
            {
                // Calculate the reflection direction
                Vector3 reflectDirection = Vector3.Reflect(direction, hit.normal);

                // Cast a new ray from the hit point in the reflection direction
                RaycastHit2D reflectHit = Physics2D.Raycast(hit.point, reflectDirection, maxDistance, mapGroundLayer | playerDummyLayer);

                // Set the positions of the LineRenderer to draw the reflected ray
                lineRenderer.SetPosition(1, reflectHit.point);

                // Check if the reflected ray hits the player dummy
                if (reflectHit.collider != null && ((1 << reflectHit.collider.gameObject.layer) & playerDummyLayer) != 0)
                {
                    playerHit = true;
                    Debug.Log("PlayerDummy is touching the reflected laser.");
                }
                else
                {
                    playerHit = false;
                }
            }
            else
            {
                playerHit = false;
            }
        }
        else
        {
            // Set the positions of the LineRenderer to draw the ray up to the maximum distance
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + direction * maxDistance);
            playerHit = false;
        }

        // Debug message to indicate that the player dummy is not touching the laser
        if (!playerHit)
        {
            Debug.Log("PlayerDummy is not touching the laser.");
        }
    }
}