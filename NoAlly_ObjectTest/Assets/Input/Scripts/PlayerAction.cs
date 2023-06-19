//日本語コメント可
using System;
using UnityEngine;
using UniRx;

public class PlayerAction
{
    public void Attack(Animator animator, WeaponType weaponType, ActorState attackState)
    {
        switch (attackState)
        {
            case ActorState.Attack:
                //if (!_ableAttack)
                {
                    Debug.Log("攻撃不能");
                    return;
                }
                Debug.Log("攻撃");
                animator.SetTrigger(weaponType.ToString());
                break;
            case ActorState.Charging:
                Debug.Log("チャージ中");
                animator.SetBool("Charging", true);
                break;
            case ActorState.ChargingAttack:
                Debug.Log("溜め攻撃");
                animator.SetBool("Charging", false);
                break;
            default:
                break;
        }
    }
}
