using UnityEngine;

public class HackerSidePlayer : MonoBehaviour
{
    public Transform realPlayer; // Reference to the real player's transform
    public float minimapScaleFactor = 0.05f; // Scale factor for the minimap (1/20th of the main map)
    public Vector3 minimapOffset; // Offset to position the minimap correctly

    // Update is called once per frame
    void Update()
    {
        // Get the real player's position in world coordinates
        Vector3 realPlayerPosition = realPlayer.position;

        // Calculate the dummy player's position on the minimap
        Vector3 minimapPosition = realPlayerPosition * minimapScaleFactor + minimapOffset;

        // Set the dummy player's position
        transform.position = minimapPosition;
    }
}