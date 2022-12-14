using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    //private bool isPlayerMoving = false;

    //public bool IsPlayerMoving { get => isPlayerMoving;}

    private void Start()
    {
        transform.position = Vector3.zero + Vector3.up * 1.5f;
    }

    void Update()
    {
        //isPlayerMoving = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            PlayerMove(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            PlayerMove(Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            PlayerMove(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            PlayerMove(Vector3.back);
        }

    }

    private void PlayerMove(Vector3 direction)
    {
        //isPlayerMoving = true;
        transform.position += direction * speed * Time.deltaTime;
        GameEvents.current.PlayerMove();
    }
}
