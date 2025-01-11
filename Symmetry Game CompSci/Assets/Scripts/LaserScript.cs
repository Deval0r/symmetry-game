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
    public int maxReflections = 5;

    void Start()
    {
        // Initialize the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.68f; // New width
        lineRenderer.endWidth = 0.68f;   // New width

        // Set the width curve to ensure the laser has a consistent width
        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0.0f, 0.68f); // New width
        widthCurve.AddKey(1.0f, 0.68f); // New width
        lineRenderer.widthCurve = widthCurve;
    }

    void Update()
    {
        // Rotate the prefab clockwise
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);

        // Direction of the ray based on the prefab's rotation
        Vector3 direction = transform.right;
        Vector3 currentPosition = transform.position;

        // Initialize the LineRenderer
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentPosition);

        int reflections = 0;
        playerHit = false;

        while (reflections < maxReflections)
        {
            // Cast the ray
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, maxDistance, mapGroundLayer | playerDummyLayer | mapMirrorLayer);

            if (hit.collider != null)
            {
                reflections++;
                Vector3 hitPoint = hit.point;
                hitPoint.z = currentPosition.z; // Ensure the reflection point is on the same z-axis

                lineRenderer.positionCount = reflections + 1;
                lineRenderer.SetPosition(reflections, hitPoint);

                // Check if the hit object is in the player dummy layer
                if (((1 << hit.collider.gameObject.layer) & playerDummyLayer) != 0)
                {
                    playerHit = true;
                    Debug.Log("PlayerDummy is touching the laser.");
                    break;
                }
                // Check if the hit object is in the map mirror layer
                else if (((1 << hit.collider.gameObject.layer) & mapMirrorLayer) != 0)
                {
                    // Calculate the reflection direction
                    direction = Vector3.Reflect(direction, hit.normal);
                    currentPosition = hitPoint;
                }
                else
                {
                    break;
                }
            }
            else
            {
                // Set the positions of the LineRenderer to draw the ray up to the maximum distance
                Vector3 endPosition = currentPosition + direction * maxDistance;
                endPosition.z = currentPosition.z; // Ensure the end point is on the same z-axis

                lineRenderer.positionCount = reflections + 2;
                lineRenderer.SetPosition(reflections + 1, endPosition);
                break;
            }
        }

        // Debug message to indicate that the player dummy is not touching the laser
        if (!playerHit)
        {
            Debug.Log("PlayerDummy is not touching the laser.");
        }
    }
}