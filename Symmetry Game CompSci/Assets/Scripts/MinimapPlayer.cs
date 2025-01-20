using UnityEngine;

/// <summary>
/// Controls the position of a minimap object relative to a target object
/// </summary>
public class MinimapPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The target object to follow")]
    private Transform targetObject;

    [SerializeField]
    [Tooltip("Offset from the target position")]
    private Vector3 offset = Vector3.zero;

    private void Update()
    {
        if (targetObject == null) return;

        // Update position to match target plus offset, with z = 0
        Vector3 newPosition = targetObject.position + offset;
        newPosition.z = 0;
        transform.position = newPosition;
            
        // Debug log positions
        Debug.Log($"Target: {targetObject.position}, Current: {transform.position}, Offset: {offset}");
    }

    private void OnDrawGizmos()
    {
        if (targetObject == null) return;

        Gizmos.color = Color.red;
        Vector3 targetPosWithOffset = targetObject.position + offset;
        targetPosWithOffset.z = 0;
        Gizmos.DrawSphere(targetPosWithOffset, 0.1f);
        Gizmos.DrawLine(targetObject.position, targetPosWithOffset);
    }
}