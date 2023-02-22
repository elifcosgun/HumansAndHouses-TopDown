using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private Stats stats;
    [SerializeField] private PlayerAgentMovement playerAgentMovement;
    [SerializeField] private CharacterStates currentCharacterState = CharacterStates.Idle;

    [SerializeField] private List<BaseAbility> PlayerSlotAbilities = new List<BaseAbility>(5);
    [SerializeField] private BaseAbility LeftMouseAbility;
    [SerializeField] private BaseAbility RightMouseAbility;

    private void Awake()
    {
        AllAbilitiesReadyToUse();
        ActionManager.OnCharacterStateChanged += SetCharacterState;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ControlAndTriggerCharacterAbility(LeftMouseAbility);
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ControlAndTriggerCharacterAbility(RightMouseAbility);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ControlAndTriggerCharacterAbility(PlayerSlotAbilities[0]);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ControlAndTriggerCharacterAbility(PlayerSlotAbilities[1]);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ControlAndTriggerCharacterAbility(PlayerSlotAbilities[2]);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ControlAndTriggerCharacterAbility(PlayerSlotAbilities[3]);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ControlAndTriggerCharacterAbility(PlayerSlotAbilities[4]);
            return;
        }
    }

    public CharacterStates CurrentCharacterState
    {
        get => currentCharacterState;
        private set => currentCharacterState = value;
    }
    public void SetCharacterState(CharacterStates newState) => CurrentCharacterState = newState;

    public void ControlAndTriggerCharacterAbility(BaseAbility ability)
    {
        if (ability == null)
            return;
        
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
}

public enum CharacterStates
{
    Idle,
    Casting
}
