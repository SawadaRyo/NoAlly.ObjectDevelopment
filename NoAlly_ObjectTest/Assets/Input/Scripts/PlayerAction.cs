//日本語コメント可
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerAction
{
    bool _ableAttack = true;

    public PlayerAction(InputController inputController)
    {
        InputEnter(inputController);
        InputUpdate(inputController);
        InputExit(inputController);
    }

    public void Attack(Animator animator, WeaponType weaponType,PlayerActionState attackState)
    {
        if (!_ableAttack) return;

        switch (attackState)
        {
            case PlayerActionState.Attack:
                Debug.Log(attackState);
                animator.SetTrigger(weaponType.ToString());
                break;
            case PlayerActionState.Charging:
                animator.SetBool("Charging", true);
                break;
            case PlayerActionState.ChargingAttack:
                animator.SetBool("Charging", false);
                break;
            default:
                break;
        }
    }

    void InputEnter(InputController inputController)
    {
        inputController.Trigger
        .OnStateEnterAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if (info.IsTag("Attack"))
            {

            }
            else if (info.IsTag("ChargeAttack"))
            {

            }
        }).AddTo(inputController.gameObject);
    }
    void InputUpdate(InputController inputController)
    {
        inputController.Trigger
        .OnStateUpdateAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if (info.IsTag("Attack"))
            {

            }
            else if (info.IsTag("ChargeAttack"))
            {

            }
        }).AddTo(inputController.gameObject);
    }
    void InputExit(InputController inputController)
    {
        inputController.Trigger
        .OnStateExitAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if (info.IsTag("Attack"))
            {

            }
            else if (info.IsTag("ChargeAttack"))
            {

            }
        }).AddTo(inputController.gameObject);
    }
}
