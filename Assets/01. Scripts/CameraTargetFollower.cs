using UnityEngine;

/// <summary>
/// Follows the target transform with an offset.
/// </summary>
public class CameraTargetFollower : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 CameraOffset;
    [SerializeField] private float CameraSpeed = 5f;

    /// <summary>
    /// Updates the position of the camera to follow the target.
    /// </summary>
    private void Update()
    {
        // Calculate the target position
        Vector3 targetPosition = Target.position + CameraOffset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, CameraSpeed * Time.deltaTime);
    }
}
