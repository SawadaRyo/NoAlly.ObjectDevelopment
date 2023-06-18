//日本語コメント可
using System;
using UnityEngine;
using UniRx;

public class PlayerAction
{
    [Tooltip("攻撃可能判定")]
    bool _ableAttack = true;
    [Tooltip("移動可能判定")]
    bool _ableMove = true;

    public bool ableMove => _ableMove;


    public PlayerAction(InputController inputController)
    {
        InputEnter(inputController);
        InputUpdate(inputController);
        InputExit(inputController);
    }

    public void Attack(Animator animator, WeaponType weaponType, PlayerActionState attackState)
    {
        switch (attackState)
        {
            case PlayerActionState.Attack:
                if (!_ableAttack)
                {
                    Debug.Log("攻撃不能");
                    return;
                }
                Debug.Log("攻撃");
                animator.SetTrigger(weaponType.ToString());
                break;
            case PlayerActionState.Charging:
                Debug.Log("チャージ中");
                animator.SetBool("Charging", true);
                break;
            case PlayerActionState.ChargingAttack:
                Debug.Log("溜め攻撃");
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
            else if (info.IsTag("FinishAttack"))
            {
                _ableAttack = false;
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
            else if (info.IsTag("FinishAttack"))
            {
                _ableAttack = true;
            }
        }).AddTo(inputController.gameObject);
    }

}
