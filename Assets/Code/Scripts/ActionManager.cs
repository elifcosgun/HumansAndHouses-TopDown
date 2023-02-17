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

    //public static Action OnPrimaryAbilityUsed;
    //public static void Fire_OnPrimaryAbilityUsed() { }

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
