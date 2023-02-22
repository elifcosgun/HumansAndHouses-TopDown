using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dash", fileName = "Ability")]
public class DashAbility : BaseAbility
{
    
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Dash Start");
    }
}
