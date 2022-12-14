using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onPlayerMove;
    public void PlayerMove()
    {
        if (onPlayerMove != null)
        {
            onPlayerMove();
        }
    }

    public event Action onPlayerStop;
    public void PlayerStop()
    {
        if (onPlayerStop != null)
        {
            onPlayerStop();
        }
    }
}
