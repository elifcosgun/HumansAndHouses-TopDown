using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerAbilityManager : MonoBehaviour
{
    //[SerializeField] private Stats stats;
    //[SerializeField] private PlayerAgentMovement playerAgentMovement;
    [SerializeField] private CharacterStates currentCharacterState = CharacterStates.Idle;

    [SerializeField] private List<BaseAbility> PlayerSlotAbilities;
    [SerializeField] private List<BaseAbility> PrimaryAbilities;
    [SerializeField] private int handIndex = 0;

    private CustomInputs input;

    private void Awake()
    {
        input = new CustomInputs();
        AllAbilitiesReadyToUse();
        ActionManager.OnCharacterStateChanged += SetCharacterState;
        ActionManager.OnPrimaryAbilityNulled += PrimaryAbilityNuller;
    }
    private void Start()
    {
        FirstPrimaryAbilitySet();
        AbilityBroadCast();
    }

    public CharacterStates CurrentCharacterState
    {
        get => currentCharacterState;
        private set => currentCharacterState = value;
    }
    public void SetCharacterState(CharacterStates newState) => CurrentCharacterState = newState;

    public void AbilityBroadCast()
    {
        ActionManager.Fire_OnAbilityBroadCast(PlayerSlotAbilities);
    }

    public void ControlAbility(InputAction.CallbackContext value)
    {
        int abilityIndex = ((int)value.ReadValue<Vector3>().y);
        if (abilityIndex <= 0)
            return;

        BaseAbility ability = PlayerSlotAbilities[abilityIndex-1];
        if (ability == null)
            return;

        PrimaryAbilitySetter(ability);
    }

    private void PrimaryAbilitySetter(BaseAbility newPrimaryAbility)
    {
        var OldAbilitySlot = PrimaryAbilities.Find(t => t == newPrimaryAbility);
        //if (OldAbilitySlot != null)
        //{
        //    ActionManager.Fire_OnPrimaryAbilityNulled(PrimaryAbilities.FindIndex(t => t == newPrimaryAbility));
        //}         // why I added this I dunno change if you see and understand
        PrimaryAbilities[handIndex] = newPrimaryAbility;
        ActionManager.Fire_OnPrimaryAbilityChanged(handIndex, newPrimaryAbility);
    }

    private void PrimaryAbilityNuller(int nulledHandIndex)
    {
        PrimaryAbilities[nulledHandIndex] = null;
    }

    public void TriggerPrimaryAbility(InputAction.CallbackContext value)
    {
        if (PrimaryAbilities[0] == null)
            return;
        BaseAbility ability = PrimaryAbilities[0];

        if (!ability.AllowedCharacterStates.Contains(currentCharacterState))
            return;

        ActionManager.Fire_OnAbilityTrigger(ability);
    }

    public void TriggerSecondaryAbility(InputAction.CallbackContext value)
    {
        if (PrimaryAbilities[1] == null)
            return;
        BaseAbility ability = PrimaryAbilities[1];

        if (!ability.AllowedCharacterStates.Contains(currentCharacterState))
            return;

        ActionManager.Fire_OnAbilityTrigger(ability);
    }


    private void PrimaryToSeconderyHandChanger(InputAction.CallbackContext value)
    {
        handIndex++;
        if (handIndex == PrimaryAbilities.Count)
        {
            handIndex = 0;
        }

        ActionManager.Fire_OnPrimaryHandChange(handIndex);
    }

    private void AllAbilitiesReadyToUse()
    {
        foreach (var ability in PlayerSlotAbilities)
        {
            ability.CurrentAbilityState = AbilityStates.ReadyToUse;
        }
    }

    private void FirstPrimaryAbilitySet()
    {
        PrimaryAbilities.Add(PlayerSlotAbilities[0]);
        PrimaryAbilities.Add(PlayerSlotAbilities[1]);

        ActionManager.Fire_OnPrimaryAbilityChanged(0, PlayerSlotAbilities[0]);
        ActionManager.Fire_OnPrimaryAbilityChanged(1, PlayerSlotAbilities[1]);
    }


    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.AbilitySelect.performed += ControlAbility;
        input.Player.PrimaryToSecondaryHand.performed += PrimaryToSeconderyHandChanger;
        input.Player.PrimaryAbilityUse.performed += TriggerPrimaryAbility;
        input.Player.SecondaryAbilityUse.performed += TriggerSecondaryAbility;
    }

    private void OnDisable()
    {
        input.Player.Disable();
        input.Player.AbilitySelect.performed -= ControlAbility;
        input.Player.PrimaryToSecondaryHand.performed -= PrimaryToSeconderyHandChanger;
        input.Player.PrimaryAbilityUse.performed -= TriggerPrimaryAbility;
        input.Player.SecondaryAbilityUse.performed -= TriggerSecondaryAbility;
    }
}

public enum CharacterStates
{
    Idle,
    Casting
}
