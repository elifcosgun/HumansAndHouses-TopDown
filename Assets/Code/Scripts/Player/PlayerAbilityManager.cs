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
    [SerializeField] private List<BaseAbility> DefaultAbilities;
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
        //List<BaseAbility> newList = new List<BaseAbility>();
        //if (DefaultAbilities != null)
        //    newList = DefaultAbilities.Concat(PlayerSlotAbilities).ToList();
        //else
        //    newList = PlayerSlotAbilities;

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

        if (DefaultAbilities != null)
            DefaultPrimarySetter(ability);
        else
            PrimaryAbilitySetter(ability);
    }

    private void DefaultPrimarySetter(BaseAbility newPrimaryAbility)
    {
        if (PrimaryAbilities[handIndex] == newPrimaryAbility)
        {
            PrimaryAbilities[handIndex] = DefaultAbilities[handIndex];
            ActionManager.Fire_OnPrimaryAbilityChanged(handIndex, DefaultAbilities[handIndex]);
            return;
        }
        else if (PrimaryAbilities[handIndex] == DefaultAbilities[handIndex])
        {
            HandAbilityDefaulter();
            PrimaryAbilities[handIndex] = newPrimaryAbility;
            ActionManager.Fire_OnPrimaryAbilityChanged(handIndex, newPrimaryAbility);
            return;
        }
        PrimaryAbilitySetter(newPrimaryAbility);
    }

    private void PrimaryAbilitySetter(BaseAbility newPrimaryAbility)
    {
        foreach (var ability in PrimaryAbilities)
        {
            if (ability == newPrimaryAbility)
            {
                SwapPrimaryAbilites();
                return;
            }
        }

        PrimaryAbilities[handIndex] = newPrimaryAbility;
        ActionManager.Fire_OnPrimaryAbilityChanged(handIndex, newPrimaryAbility);
    }

    private void SwapPrimaryAbilites()
    {
        var temporaryAbility = PrimaryAbilities[0];
        PrimaryAbilities[0] = PrimaryAbilities[1];
        PrimaryAbilities[1] = temporaryAbility;

        ActionManager.Fire_OnPrimaryAbilityChanged(0, PrimaryAbilities[0]);
        ActionManager.Fire_OnPrimaryAbilityChanged(1, PrimaryAbilities[1]);

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
        if (DefaultAbilities != null)
        {
            PrimaryAbilities[0] = DefaultAbilities[0];
            ActionManager.Fire_OnPrimaryAbilityChanged(0, DefaultAbilities[0]);
        }
    }

    public void TriggerSecondaryAbility(InputAction.CallbackContext value)
    {
        if (PrimaryAbilities[1] == null)
            return;
        BaseAbility ability = PrimaryAbilities[1];

        if (!ability.AllowedCharacterStates.Contains(currentCharacterState))
            return;

        ActionManager.Fire_OnAbilityTrigger(ability);
        if (DefaultAbilities != null)
        {
            PrimaryAbilities[1] = DefaultAbilities[1];
            ActionManager.Fire_OnPrimaryAbilityChanged(1, DefaultAbilities[1]);
        }
    }


                // Q and E keys changes the main hand
    private void PrimaryHandChangerQ(InputAction.CallbackContext value)
    {
        handIndex = 0;
        ActionManager.Fire_OnPrimaryHandChange(handIndex);
    }

    private void SecondaryHandChangerE(InputAction.CallbackContext value)
    {
        handIndex = 1;
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
        if (DefaultAbilities != null)
        {
            PrimaryAbilities.Add(DefaultAbilities[0]);
            PrimaryAbilities.Add(DefaultAbilities[1]);

            ActionManager.Fire_OnPrimaryAbilityChanged(0, DefaultAbilities[0]);
            ActionManager.Fire_OnPrimaryAbilityChanged(1, DefaultAbilities[1]);
        }
        else
        {
            PrimaryAbilities.Add(PlayerSlotAbilities[0]);
            PrimaryAbilities.Add(PlayerSlotAbilities[1]);

            ActionManager.Fire_OnPrimaryAbilityChanged(0, PlayerSlotAbilities[0]);
            ActionManager.Fire_OnPrimaryAbilityChanged(1, PlayerSlotAbilities[1]);
        }
    }

    private void HandAbilityDefaulter()
    {
        PrimaryAbilities[0] = DefaultAbilities[0];
        PrimaryAbilities[1] = DefaultAbilities[1];

        ActionManager.Fire_OnPrimaryAbilityChanged(0, DefaultAbilities[0]);
        ActionManager.Fire_OnPrimaryAbilityChanged(1, DefaultAbilities[1]);
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.AbilitySelect.performed += ControlAbility;
        input.Player.PrimaryHand.performed += PrimaryHandChangerQ;
        input.Player.SecondaryHand.performed += SecondaryHandChangerE;
        input.Player.PrimaryAbilityUse.performed += TriggerPrimaryAbility;
        input.Player.SecondaryAbilityUse.performed += TriggerSecondaryAbility;
    }

    private void OnDisable()
    {
        input.Player.Disable();
        input.Player.AbilitySelect.performed -= ControlAbility;
        input.Player.PrimaryHand.performed -= PrimaryHandChangerQ;
        input.Player.SecondaryHand.performed -= SecondaryHandChangerE;
        input.Player.PrimaryAbilityUse.performed -= TriggerPrimaryAbility;
        input.Player.SecondaryAbilityUse.performed -= TriggerSecondaryAbility;
    }
}

public enum CharacterStates
{
    Idle,
    Casting
}
