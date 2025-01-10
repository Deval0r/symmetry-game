using UnityEngine;

public class MinimapBlock : MonoBehaviour
{
    public Transform realBlock; // Reference to the real block's transform
    public float minimapScaleFactor = 0.05f; // Scale factor for the minimap (1/20th of the main map)
    public Vector3 minimapOffset; // Offset to position the minimap correctly

    // Update is called once per frame
    void Update()
    {
        // Get the real block's position in world coordinates
        Vector3 realBlockPosition = realBlock.position;

        // Calculate the block's position on the minimap
        Vector3 minimapPosition = realBlockPosition * minimapScaleFactor + minimapOffset;

        // Set the block's position
        transform.position = minimapPosition;
    }
}