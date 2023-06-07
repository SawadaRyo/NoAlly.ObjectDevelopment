//日本語コメント可
using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class PlayerMove
{
    public PlayerMove(InputController inputController)
    {
        IDisposable enterState = inputController.Trigger
        .OnStateEnterAsObservable()  //Animationの遷移開始を検知
        .Subscribe(onStateInfo =>
        {
            AnimatorStateInfo info = onStateInfo.StateInfo; //現在のAnimatorの遷移状況
            if (info.IsTag(""))
            {

            }
        }).AddTo(inputController.gameObject);
    }
    public void Move(float x,float y,float speed,Rigidbody rb)
    {
        var moveVec = x * speed;
        rb.velocity = new Vector3(moveVec, rb.velocity.y, 0f);
    }

    
}
