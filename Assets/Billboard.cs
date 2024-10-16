using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Billboard : MonoBehaviour
{
    [Header("Billboard Settings")]
    [SerializeField] private bool lookAtCamera = false;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationAngle = 50f;

    private void Update()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.localScale = new Vector3(lookAtCamera ? -Mathf.Abs(transform.localScale.x) : transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
