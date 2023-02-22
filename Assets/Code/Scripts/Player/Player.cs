using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterStates currentCharacterState = CharacterStates.Idle;

    public CharacterStates CurrentCharacterState
    {
        get => currentCharacterState;
        private set => currentCharacterState = value;
    }

    public void SetCharacterState(CharacterStates newState) => CurrentCharacterState = newState;
}
