//日本語コメント可
using System;
using UnityEngine;
using UniRx;

public class AttackState : IInputState<InputController>
{
    [Tooltip("攻撃可能判定")]
    bool _ableAttack = true;
    [Tooltip("移動可能判定")]
    bool _ableMove = true;

    public bool ableMove => _ableMove;

    public void ActorAnimationState(InputController inputController)
    {
        InputEnter(inputController);
        InputUpdate(inputController);
        InputExit(inputController);
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
                
            }
        }).AddTo(inputController.gameObject);
    }
}
