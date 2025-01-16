using UnityEngine;

public class MinimapPositioning : MonoBehaviour
{
    public Transform minimapCenter;   // Reference to the minimap's center point (e.g., the player icon)
    public Transform realObject;      // Reference to the real object in the game world
    public float minimapScaleFactor = 0.05f; // Scale factor for the minimap

    // Update is called once per frame
    void Update()
    {
        if (realObject == null || minimapCenter == null)
        {
            Debug.LogWarning("RealObject or MinimapCenter is not assigned.");
            return;
        }

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
