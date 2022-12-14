using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RotationMovement : MonoBehaviour
{
    Vector3 lookPosition;
    Vector3 lookDirection;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPosition = hit.point;
        }

        lookDirection = lookPosition - transform.position;
        //lookDirection.y = 0;
        
        //transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    private void FixedUpdate()
    {
        Vector3 lookDirY = Vector3.zero;
        lookDirY.y = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(lookDirY);
    }
}
