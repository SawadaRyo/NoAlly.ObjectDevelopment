//日本語コメント可
using System;
using UnityEngine;
using UniRx;

public class MoveState : IInputState<InputController>
{
    public void ActorAnimationState(InputController owner)
    {
        IDisposable enterState = owner.Trigger
        .OnStateEnterAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if (info.IsTag(""))
            {

            }
        }).AddTo(owner.gameObject);
    }
}
