using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AbilityUIController : MonoBehaviour
{
    [SerializeField] private GameObject AbilityUI;
    [SerializeField] private int AbilityXPosPlus = 120;

    [SerializeField] private List<AbilityIcon> PrimaryAbilityUIs;

    [SerializeField]  private List<AbilityIcon> HandUIs = new List<AbilityIcon>();
    private List<AbilityIcon> AbilityUIs = new List<AbilityIcon>();

    private void Awake()
    {
        ActionManager.OnAbilityBroadCast += AbilityUICreator;
        ActionManager.OnPrimaryAbilityChanged += PrimaryAbilitiesIconChanged;
        ActionManager.OnPrimaryAbilityNulled += PrimaryAbilitiesIconNulled;
        ActionManager.OnPrimaryHandChange += PrimaryHandChanged;

        ActionManager.OnAbilityTrigger += AbilityPercentFill;
    }

    private void AbilityUICreator(List<BaseAbility> abilities)
    {
        HandUIs[0].Selected.DOFade(255f, 0.000001f);
        HandUIs[1].Selected.DOFade(0f, 0.000001f);

        foreach (BaseAbility ability in abilities)
        {
            var abilityUI = Instantiate(AbilityUI, this.transform);

            AbilityIcon abilityIcon = abilityUI.GetComponent<AbilityIcon>();
            abilityIcon.Icon.sprite = ability.AbilityImage;
            abilityIcon.Selected.DOFade(0, 0.001f);
            AbilityUIs.Add(abilityIcon);
            abilityUI.transform.position = transform.position + Vector3.right * AbilityXPosPlus * (AbilityUIs.Count -1);
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

    private void PrimaryHandsAbilityChanged(int iconIndex)
    {
        // primary and secondary hands skill will have a frame that shows which hand holding that skill
    }

    private void PrimaryHandChanged(int handIndex)
    {
        foreach (var hand in HandUIs)
        {
            hand.Selected.DOFade(0f, 0.00001f);
        }

        HandUIs[handIndex].Selected.DOFade(255f, 0.00001f);
    }

    private void AbilityPercentFill(BaseAbility ability)
    {
        float fillTime = ability.CastingTime + ability.Cooldown;

        

    }
}
