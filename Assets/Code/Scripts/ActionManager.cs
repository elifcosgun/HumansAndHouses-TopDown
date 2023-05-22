using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ActionManager : MonoBehaviour
{
    //public static Action<GameObject> OnEnemyKilled;
    //public static void Fire_OnEnemyKilled(GameObject obj) { OnEnemyKilled?.Invoke(obj); }

    //public static Action<float, float> OnAbilityUsed;

    //public static void Fire_OnAbilityUsed(float activeTime, float cooldownTime) { OnAbilityUsed?.Invoke(activeTime, cooldownTime); }

    public static Action<List<BaseAbility>> OnAbilityBroadCast;
    public static void Fire_OnAbilityBroadCast(List<BaseAbility> baseAbilities) { OnAbilityBroadCast?.Invoke(baseAbilities); }


    public static Action<CharacterStates> OnCharacterStateChanged;
    public static void Fire_OnCharacterStateChanged(CharacterStates characterState) { OnCharacterStateChanged?.Invoke(characterState); }


    public static Action<int, BaseAbility> OnPrimaryAbilityChanged;
    public static void Fire_OnPrimaryAbilityChanged(int mainAbilityIndex, BaseAbility ability) { OnPrimaryAbilityChanged?.Invoke(mainAbilityIndex, ability); }


    public static Action<int> OnPrimaryAbilityNulled;
    public static void Fire_OnPrimaryAbilityNulled(int nullifiedAbilityIndex) { OnPrimaryAbilityNulled?.Invoke(nullifiedAbilityIndex); }


    public static Action<int> OnPrimaryHandChange;
    public static void Fire_OnPrimaryHandChange(int handIndex) { OnPrimaryHandChange?.Invoke(handIndex); }


    public static Action<BaseAbility> OnAbilityTrigger;
    public static void Fire_OnAbilityTrigger(BaseAbility baseAbility) { OnAbilityTrigger?.Invoke(baseAbility); }


    //Animations

    public static Action<float, float> OnAnimationSetMove;
    public static void Fire_OnanimationSetMove(float valueX, float valueY) { OnAnimationSetMove?.Invoke(valueX, valueY); }


    public static Action<float> OnAnimationSetRun;
    public static void Fire_OnAnimationSetRun(float valueRun) { OnAnimationSetRun?.Invoke(valueRun); }


    public void OnDestroy()
    {
        FieldInfo[] fieldInfos = this.GetType().GetFields();
        // Debug.Log("fieldInfos " + fieldInfos.Length);
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            // Debug.Log(fieldInfos[i].Name);

            fieldInfos[i].SetValue(this, null);
        }
    }
}
