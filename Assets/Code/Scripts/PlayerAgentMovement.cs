using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private bool isMoving { get; set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude <= 0)
        {
            //anim setbool wwalk false
            //return
        }

        if (Mathf.Abs(input.y) >= 0.01f || Mathf.Abs(input.x) >= 0.01f)
        {
            Move(input);
        }
    }

    private void Move(Vector2 input)
    {
        //anim walk true
        Vector3 destination = transform.position + Vector3.right * input.x + Vector3.forward * input.y;
        navMeshAgent.destination = destination;
        //navMeshAgent.Move(destination);
    }

    
}
