using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RotationMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 4f;

    Vector3 lookPosition;
    Vector3 lookDirection;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPosition = hit.point;
        }

        lookDirection = lookPosition - transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 lookDirY = Vector3.zero;
        lookDirY.y = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(lookDirY);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookDirY), rotationSpeed * Time.deltaTime);
    }
}
