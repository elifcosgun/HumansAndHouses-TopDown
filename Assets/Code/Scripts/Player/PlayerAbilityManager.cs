using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityManager : MonoBehaviour
{
    //[SerializeField] private Stats stats;
    //[SerializeField] private PlayerAgentMovement playerAgentMovement;
    [SerializeField] private CharacterStates currentCharacterState = CharacterStates.Idle;

    [SerializeField] private List<BaseAbility> PlayerSlotAbilities = new List<BaseAbility>(7);

    private CustomInputs input;

    private void Awake()
    {
        AllAbilitiesReadyToUse();
        ActionManager.OnCharacterStateChanged += SetCharacterState;
        input = new CustomInputs();
    }

    public CharacterStates CurrentCharacterState
    {
        get => currentCharacterState;
        private set => currentCharacterState = value;
    }
    public void SetCharacterState(CharacterStates newState) => CurrentCharacterState = newState;

    public void ControlAbility(InputAction.CallbackContext value)
    {
        int abilityIndex = ((int)value.ReadValue<Vector3>().y);
        if (abilityIndex < 0)
            return;

        BaseAbility ability = PlayerSlotAbilities[abilityIndex];
        if (ability == null)
            return;

        TriggerAbility(ability);
    }

    public void TriggerAbility(BaseAbility ability)
    {
        if (!ability.AllowedCharacterStates.Contains(currentCharacterState))
            return;

        ActionManager.Fire_OnAbilityTrigger(ability);
    }

    private void AllAbilitiesReadyToUse()
    {
        foreach (var ability in PlayerSlotAbilities)
        {
            ability.CurrentAbilityState = AbilityStates.ReadyToUse;
        }
    }
    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.AbilityUse.performed += ControlAbility;

    }

    private void OnDisable()
    {
        input.Player.Disable();
        input.Player.AbilityUse.performed -= ControlAbility;
    }
}

public enum CharacterStates
{
    Idle,
    Casting
}
