//日本語コメント可
using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class PlayerMove
{
    Vector2 _velo = Vector2.zero;
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
    public void Move(float h, float v, float speed, Rigidbody rb)//プレイヤーの方向転換
    {
        float moveSpeed = 0f;
        //プレイヤーの移動
        if (_animState.AbleMove)
        {
            if (IsGrounded() && IsWalled(CurrentNormal(new Vector2(h, v))) == PlayerClimbWall.NONE)
            {
                if (dash)
                {
                    moveSpeed = _dashSpeed;
                }
                else
                {
                    moveSpeed = _speed;
                }
                //_beforeSpeed = moveSpeed;
            }
            else if (!IsGrounded() && IsWalled(CurrentNormal(new Vector2(h, v))) == PlayerClimbWall.NONE)
            {
                if (dash)
                {
                    moveSpeed = _dashSpeed;
                }
                else if (!dash || IsWalled(CurrentNormal(new Vector2(h, v))) != PlayerClimbWall.NONE)
                {
                    moveSpeed = _speed;
                }
            }

            Vector3 normalVector = _hitInfo.normal;
            if (_ableInput)
            {
                Vector3 onPlane = Vector3.ProjectOnPlane(new Vector3(h, 0f, 0f), normalVector);
                _velo.x = onPlane.x * moveSpeed;
                _velo.y = onPlane.y * moveSpeed;
                if (Mathf.Abs(_velo.y) <= 0.01f)
                {
                    rb.velocity = new Vector3(_velo.x, rb.velocity.y, 0);
                }
                else if (Mathf.Abs(_velo.y) > 0.01f && !_isJump)
                {
                    rb.velocity = new Vector3(_velo.x, _velo.y, 0);
                }
                _animator.SetFloat("MoveSpeed", Mathf.Abs(_velo.normalized.x * moveSpeed));
            }
            else
            {
                _animator.SetFloat("MoveSpeed", 0);
            }
        }

    }
    void RotateMethod(Transform targetObj,PlayerActionState playerVec,float turnSpeed)
    {
        switch(playerVec)
        {
            case PlayerActionState.Right:
                Quaternion rotationRight = Quaternion.LookRotation(Vector3.right);
                targetObj.rotation = Quaternion.Slerp(targetObj.rotation, rotationRight, Time.deltaTime * turnSpeed);
                break;
            case PlayerActionState.Left:
                Quaternion rotationLeft = Quaternion.LookRotation(Vector3.left);
                targetObj.rotation = Quaternion.Slerp(targetObj.rotation, rotationLeft, Time.deltaTime * turnSpeed);
                break;
            case PlayerActionState.Upper:
                Quaternion rotationUp = Quaternion.LookRotation(Vector3.zero);
                targetObj.rotation = Quaternion.Slerp(targetObj.rotation, rotationUp, Time.deltaTime * turnSpeed);
                break;
            case PlayerActionState.Down:
                break;
        }
    }
}
