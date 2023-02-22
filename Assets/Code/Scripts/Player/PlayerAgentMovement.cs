using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentMovement : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 3.5f;
    [SerializeField] private float RunSpeed = 7f;

    private NavMeshAgent navMeshAgent;

    private bool isRun = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isRun = Input.GetKey(KeyCode.LeftShift);

        if (input.magnitude <= 0)
        {
            Stop();
            return;
        }

        if (Mathf.Abs(input.y) >= 0.01f || Mathf.Abs(input.x) >= 0.01f)
        {
            Move(input);
        }
    }

    private void Move(Vector2 input)
    {
        Vector3 destination = transform.position + Vector3.right * input.x + Vector3.forward * input.y;
        navMeshAgent.destination = destination;

        if (isRun)
            navMeshAgent.speed = RunSpeed;
        else
            navMeshAgent.speed = normalSpeed;

        ActionManager.Fire_OnanimationSetMove(input.x, input.y, isRun);
    }

    private void Stop()
    {
        ActionManager.Fire_OnanimationSetMove(0f, 0f, isRun);
    }
}
