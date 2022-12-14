using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMovementRigidbody : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    Rigidbody rb;
    Vector3 movement = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
