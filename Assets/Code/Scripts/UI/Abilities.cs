using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Abilities : MonoBehaviour
{
    [SerializeField] private GameObject AbilityUI;
    [SerializeField] private int AbilityXPosPlus = 120;

    [SerializeField] private List<AbilityIcon> PrimaryAbilityUIs;

    private List<AbilityIcon> AbilityUIs = new List<AbilityIcon>();

    private void Awake()
    {
        ActionManager.OnAbilityBroadCast += AbilityUICreator;
        ActionManager.OnPrimaryAbilityChanged += PrimaryAbilitiesIconChanged;
        ActionManager.OnPrimaryAbilityNulled += PrimaryAbilitiesIconNulled;
    }

    private void AbilityUICreator(List<BaseAbility> abilities)
    {
        var abilityUI = Instantiate(AbilityUI, this.transform);

        AbilityIcon abilityIcon = abilityUI.GetComponent<AbilityIcon>();

        foreach (BaseAbility ability in abilities)
        {
            abilityIcon.Icon.sprite = ability.AbilityImage;

            abilityIcon.Selected.DOFade(0, 0.001f);

            AbilityUIs.Add(abilityIcon);

            abilityUI.transform.position = transform.position + Vector3.right * AbilityXPosPlus * AbilityUIs.Count;
        }
    }

    private void PrimaryAbilitiesIconChanged(int iconIndex, BaseAbility ability)
    {
        PrimaryAbilityUIs[iconIndex].Icon.sprite = ability.AbilityImage;
    }
    private void PrimaryAbilitiesIconNulled(int iconIndex)
    {
        PrimaryAbilityUIs[iconIndex].Icon = null;
    }
}
