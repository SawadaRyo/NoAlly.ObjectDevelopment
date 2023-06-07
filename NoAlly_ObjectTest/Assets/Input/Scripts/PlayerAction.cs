//日本語コメント可
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerAction
{
    public PlayerAction(InputController inputController)
    {
        IDisposable enterState = inputController.Trigger
        .OnStateEnterAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if(info.IsTag(""))
            {

            }
        }).AddTo(inputController);
    }
    public void Attack(Animator animator, WeaponType weaponType,PlayerActionState playerActionState)
    {
        switch (playerActionState)
        {
            case PlayerActionState.Attack:
                Debug.Log(playerActionState);
                animator.SetTrigger(weaponType.ToString());
                break;
            case PlayerActionState.Attacking:
                break;
            default:
                break;
        }
    }
}
