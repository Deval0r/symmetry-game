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

        // Cast the initial laser
        CastLaser(transform.position, transform.right, maxReflections);
    }

    void CastLaser(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        Vector3[] positions = new Vector3[(maxReflections + 1) * 2];
        int positionIndex = 0;

        while (reflectionsRemaining > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance, mapGroundLayer | playerDummyLayer | mapMirrorLayer);
            positions[positionIndex++] = position;

            if (hit.collider != null)
            {
                positions[positionIndex++] = hit.point;

                if (((1 << hit.collider.gameObject.layer) & playerDummyLayer) != 0)
                {
                    // PlayerDummy hit
                    playerHit = true;
                    Debug.Log("PlayerDummy hit!");
                    break;
                }

                if (((1 << hit.collider.gameObject.layer) & mapMirrorLayer) != 0)
                {
                    // Reflect the laser
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                    reflectionsRemaining--;
                }
                else
                {
                    break;
                }
            }
            else
            {
                positions[positionIndex++] = position + direction * maxDistance;
                break;
            }
        }

        lineRenderer.positionCount = positionIndex;
        lineRenderer.SetPositions(positions);
    }
}