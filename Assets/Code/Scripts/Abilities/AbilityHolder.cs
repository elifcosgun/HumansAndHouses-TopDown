using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityHolder : MonoBehaviour
{
    public CharacterStates OwnerCurrentCharacterState;
    public BaseAbility Ability;
    public AbilityStates CurrentAbilityState = AbilityStates.ReadyToUse;

    
    private IEnumerator handleAbilityUsage;

    private void Awake()
    {
        ActionManager.OnCharacterStateChanged += (CharacterStates characterState) =>
        {
            OwnerCurrentCharacterState = characterState;
        };
    }

    public enum AbilityStates
    {
        ReadyToUse = 0,
        Casting = 1,
        Cooldown = 2
    }

    public void TriggerAbility()
    {
        if (CurrentAbilityState != AbilityStates.ReadyToUse)
            return;

        if (!CharacterIsOnAllowedState())
            return;

        //handleAbilityUsage = 
    }

    public bool CharacterIsOnAllowedState()
    {
        return Ability.AllowedCharacterStates.Contains(OwnerCurrentCharacterState);
    }

    
}
