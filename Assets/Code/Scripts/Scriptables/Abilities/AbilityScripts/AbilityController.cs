using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class AbilityController : MonoBehaviour
{
    private void Awake()
    {
        ActionManager.OnAbilityTrigger += TriggerAbility;
        ActionManager.Fire_OnCharacterStateChanged(CharacterStates.Idle);
    }

    public void TriggerAbility(BaseAbility ability)
    {
        if (ability.CurrentAbilityState != AbilityStates.ReadyToUse /*&& Abilities.Find(t => t.name == ability.name) != null*/)
            //TODO: ability cant be used
            return;


        StartCoroutine(WaitForAbility());
        IEnumerator WaitForAbility()
        {
            ability.CurrentAbilityState = AbilityStates.Casting;
            ActionManager.Fire_OnCharacterStateChanged(CharacterStates.Casting);
            yield return new WaitForSeconds(ability.CastingTime);

            ability.Activate();

            ability.CurrentAbilityState = AbilityStates.Cooldown;
            yield return new WaitForSeconds(ability.Cooldown);

            ability.CurrentAbilityState = AbilityStates.ReadyToUse;
            ActionManager.Fire_OnCharacterStateChanged(CharacterStates.Idle);
        }
    }
}
