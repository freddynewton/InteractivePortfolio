using UnityEngine;

/// <summary>
/// Transfers the position of an object (e.g., the player or another Transform) to a shader,
/// allowing the shader to access this position for dynamic effects.
/// </summary>
public class AddPositionToShader : MonoBehaviour
{
    // Reference to the Transform of the object (e.g., the player) whose position will be passed to the shader.
    [SerializeField] private Transform _transform;

    // Reference to the material that contains the shader to which the position will be passed.
    [SerializeField] private Material _material;

    /// <summary>
    /// This method is called once per frame and updates the object's position in the shader.
    /// </summary>
    private void Update()
    {
        // Check if both the material and the Transform are assigned before passing the position.
        if (_material != null && _transform != null)
        {
            // Pass the object's position to the shader as a Vector3 (e.g., "_TransformPosition").
            _material.SetVector("_TransformPosition", _transform.position);
        }
    }
}
