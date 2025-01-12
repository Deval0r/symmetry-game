using UnityEngine;

public class MinimapPositioning : MonoBehaviour
{
    public Transform realObject;      // Reference to the real object's transform
    public Transform minimapCenter;   // Reference to the minimap's center point (e.g., the player icon)
    public float minimapScaleFactor = 0.05f; // Scale factor for the minimap

    // Update is called once per frame
    void Update()
    {
        // Calculate the position relative to the minimap center
        Vector3 relativePosition = realObject.position - minimapCenter.position;

        // Scale the relative position
        Vector3 scaledPosition = relativePosition * minimapScaleFactor;

        // Calculate the minimap icon's position
        Vector3 minimapPosition = minimapCenter.position + scaledPosition;

        // Set the icon's position
        transform.position = minimapPosition;
    }
}
