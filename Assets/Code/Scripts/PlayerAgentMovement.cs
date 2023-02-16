using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private AnimationSystemBase animationSystemBase;
    private bool isMoving { get; set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationSystemBase = GetComponentInChildren<AnimationSystemBase>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isRun = Input.GetKeyDown(KeyCode.LeftShift);

        if (input.magnitude <= 0)
        {
            Stop();
            return;
        }

        if (Mathf.Abs(input.y) >= 0.01f || Mathf.Abs(input.x) >= 0.01f)
        {

            Move(input, isRun);
        }
    }

    private void Move(Vector2 input, bool isRun)
    {
        Vector3 destination = transform.position + Vector3.right * input.x + Vector3.forward * input.y;
        navMeshAgent.destination = destination;

        animationSystemBase.SetMove(AnimationKey.Xaxis, input.x, AnimationKey.Yaxis, input.y, isRun);
    }

    private void Stop()
    {
        animationSystemBase.SetMove(AnimationKey.Xaxis, 0f, AnimationKey.Yaxis, 0f, AnimationKey.WalkBlend, 0f);
    }
}
