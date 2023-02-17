using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityHolder : MonoBehaviour
{
    public CharacterStates Owner;
    public BaseAbility Ability;
    public AbilityStates CurrentAbilityState = AbilityStates.ReadyToUse;
    public UnityEvent OnTriggerAbility;
    
    private IEnumerator _handleAbilityUsage;

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
    }

    public bool CharacterIsOnAllowedState()
    {
        return true; //Ability.AllowedCharacterStates.Contains(Owner.CurrentCharacterState)
    }
}
