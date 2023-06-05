//日本語コメント可
using UnityEngine;
using UniRx;

public static class PlayerMove
{
    public static void Move(float x,float y,float speed,Rigidbody rb)
    {
        var moveVec = x * speed;
        rb.velocity = new Vector3(moveVec, rb.velocity.y, 0f);
    }
}
