using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    public LayerMask mapGroundLayer; // Layer mask for the mapGround layer
    public LayerMask playerDummyLayer; // Layer mask for the player dummy layer
    public float maxDistance = 100f; // Maximum distance the ray can travel
    public bool playerHit = false; // Boolean to indicate if the player is hit

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        // Set the number of positions to 2 (start and end of the line)
        lineRenderer.positionCount = 2;
        // Set the width of the line
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // Set the color of the line
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        // Direction of the ray based on the prefab's rotation
        Vector3 direction = transform.right;

        // Cast the ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, mapGroundLayer | playerDummyLayer);

        // If the ray hits an object with the mapGround layer or the player dummy layer
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