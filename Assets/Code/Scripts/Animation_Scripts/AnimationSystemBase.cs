using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AnimationSystemBase : MonoBehaviour
{
    [HideInInspector] private Animator animator;
    // example of list structs //public AnimationKey[] animationKeys;
    //[SerializeField] private float attackSpeed;

    //private CharacterBase characterBase;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        ActionManager.OnAnimationSetMove += SetMove;
        ActionManager.OnAnimationSetRun += SetRun;
    }

    protected virtual void Start()
    {
        //SetAttackSpeed(1);
    }

    //public AnimationSystemBase Init(CharacterBase characterBase)
    //{
    //    this.characterBase = characterBase;
    //    return this;
    //}

    public virtual void AttackAnimation(string key, bool isTrue = false, AnimationClip animationClip = null)
    {
        if (!animator)
            return;

        animator.SetTrigger(key);
    }

    public virtual void SetMove(float valueX, float valueY)
    {
        if (!animator)
            return;

        animator.SetFloat(AnimationKey.Xaxis, valueX);
        animator.SetFloat(AnimationKey.Yaxis, valueY);
    }

    public virtual void SetRun(float valueRun)
    {
        if (!animator)
            return;

        animator.SetFloat(AnimationKey.WalkBlend, valueRun);
    }

    //public virtual void SkillAnimation(string key, AnimationClip animationClip = null)
    //{
    //    if (!animator)
    //        return;

    //    animator.SetTrigger(key);
    //}

    public virtual void SetBool(string key, bool value)
    {
        if (!animator)
            return;

        animator.SetBool(key, value);
    }

    public virtual void SetFloat(string key, float value)
    {
        if (!animator)
            return;

        animator.SetFloat(key, value);
    }

    public virtual void SetTrigger(string key)
    {
        if (!animator)
            return;

        animator.SetTrigger(key);
    }

    //private Coroutine temporaryAttackSpeed, temporaryMovementSpeed;
    //private GameObject effectObject;

    //public void SetAttackSpeed(float effect, bool isTemporary = false, float duration = 0, GameObject slowObject = null)
    //{
    //    animator.SetFloat(AnimationKey.AttackSpeed, attackSpeed * effect);

    //    if (isTemporary && gameObject.activeInHierarchy)
    //    {
    //        if (temporaryAttackSpeed != null)
    //            StopCoroutine(temporaryAttackSpeed);

    //        temporaryAttackSpeed = StartCoroutine(Duration());

    //        //if (!effectObject)
    //        //{
    //        //    effectObject = Instantiate(slowObject, transform);
    //        //    effectObject.transform.position = characterBase.posParticle.position;
    //        //}

    //        IEnumerator Duration()
    //        {
    //            yield return new WaitForSeconds(duration);
    //            animator.SetFloat(AnimationKey.AttackSpeed, attackSpeed);
    //            if (effectObject)
    //                Destroy(effectObject);
    //        }
    //    }
    //}

    //public void SetMovementSpeed(float effect, bool isTemporary = false, float duration = 0)
    //{
    //    animator.SetFloat(AnimationKey.MovementSpeed, effect);

    //    if (isTemporary && gameObject.activeInHierarchy)
    //    {
    //        if (temporaryMovementSpeed != null)
    //            StopCoroutine(temporaryMovementSpeed);

    //        temporaryMovementSpeed = StartCoroutine(Duration());

    //        IEnumerator Duration()
    //        {
    //            yield return new WaitForSeconds(duration);
    //            animator.SetFloat(AnimationKey.MovementSpeed, 1);
    //        }
    //    }
    //}

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    //private void OnDisable()
    //{
    //    StopAllCoroutines();

    //    if (effectObject)
    //        Destroy(effectObject);

    //    animator.SetFloat(AnimationKey.AttackSpeed, attackSpeed);
    //    animator.SetFloat(AnimationKey.MovementSpeed, 1);
    //}

    //public virtual void ReviewAnimation()
    //{
    //    if (!animator)
    //        return;

    //    animator.SetTrigger(AnimationKey.Review.ToString());
    //}
}

[System.Serializable]
public struct AnimationKey
{
    public const string WalkBlend = "WalkBlend";
    public const string Xaxis = "Xaxis";
    public const string Yaxis = "Yaxis";

}