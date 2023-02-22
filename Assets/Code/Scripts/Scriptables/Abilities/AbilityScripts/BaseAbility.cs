using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [Header("Cooldown And Casting Time")]
    public AbilityStates CurrentAbilityState = AbilityStates.ReadyToUse;

    public float Cooldown = 1f;
    public float CastingTime = 0f;


    [Header("Allowed States")]
    public List<CharacterStates> AllowedCharacterStates = new
        List<CharacterStates>() { CharacterStates.Idle };


    public virtual void Activate()
    {

    }
}

public enum AbilityStates
{
    ReadyToUse = 0,
    Casting = 1,
    Cooldown = 2
}
