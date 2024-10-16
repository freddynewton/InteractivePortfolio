using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 5f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * _movementSpeed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + movement);
    }
}
