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
        widthCurve.AddKey(0.0f, 0.68f);
        widthCurve.AddKey(1.0f, 0.68f);
        lineRenderer.widthCurve = widthCurve;

        // Set the material and color of the LineRenderer
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green; // Change to desired color
        lineRenderer.endColor = Color.green;   // Change to desired color
    }

    void Update()
    {
        // Rotate the laser
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Cast the laser and update the LineRenderer positions
        CastLaser();
    }

    void CastLaser()
    {
        Vector3 direction = transform.right;
        Vector3 currentPosition = transform.position;
        lineRenderer.SetPosition(0, currentPosition);

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, maxDistance, mapGroundLayer | playerDummyLayer | mapMirrorLayer);
            if (hit.collider != null)
            {
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, hit.point);
                currentPosition = hit.point;

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("mapMirrorLayer"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                }
                else
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("playerDummyLayer"))
                    {
                        playerHit = true;
                    }
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount = i + 2;
                lineRenderer.SetPosition(i + 1, currentPosition + direction * maxDistance);
                break;
            }
        }
    }
}