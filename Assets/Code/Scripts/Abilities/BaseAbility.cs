using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [Header("Cooldown And Casting Time")]
    public bool HasCooldown = true;
    public float Cooldown = 1f;
    public float CastingTime = 0f;

    [Header("Allowed States")]
    public List<CharacterStates> AllowedCharacterStates = new
        List<CharacterStates>() { CharacterStates.Idle };

    //public virtual void OnAbilityUpdate()
}

public enum CharacterStates
{
    Idle,
    Walking
}
