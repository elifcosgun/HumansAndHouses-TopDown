using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAgentMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float normalSpeed = 3.5f;
    [SerializeField] private float RunSpeed = 7f;

    private NavMeshAgent navMeshAgent;
    private Vector2 moveVector;

    private CustomInputs inputPlayer;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        inputPlayer = new CustomInputs();
    }

    private void Update()
    {
        if (moveVector == Vector2.zero)
            return;
        Vector3 destination = transform.position + Vector3.right * moveVector.x + Vector3.forward * moveVector.y;
        navMeshAgent.destination = destination;
    }

    private void OnMove(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        ActionManager.Fire_OnanimationSetMove(moveVector.x, moveVector.y);
    }

    private void OnStop(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        ActionManager.Fire_OnanimationSetMove(0f, 0f);
    }

    private void OnRun(InputAction.CallbackContext value)
    {
        float valueRun = value.ReadValue<float>();
        moveSpeed = normalSpeed + RunSpeed * valueRun;
        navMeshAgent.speed = moveSpeed;
        ActionManager.Fire_OnAnimationSetRun(valueRun);
    }

    private void OnEnable()
    {
        inputPlayer.Player.Enable();
        inputPlayer.Player.Movement.performed += OnMove;
        inputPlayer.Player.Movement.canceled += OnStop;
        inputPlayer.Player.Run.performed += OnRun;
        inputPlayer.Player.Run.canceled += OnRun;

    }

    private void OnDisable()
    {
        inputPlayer.Player.Disable();
        inputPlayer.Player.Movement.performed -= OnMove;
        inputPlayer.Player.Movement.canceled -= OnStop;
        inputPlayer.Player.Run.performed -= OnRun;
        inputPlayer.Player.Run.canceled -= OnRun;

    }
}
