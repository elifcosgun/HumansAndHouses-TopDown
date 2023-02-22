using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NormalAttack", fileName = "Ability")]
public class NormalAttackAbility : BaseAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Normal Attack Start");
    }
}
