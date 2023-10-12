//日本語コメント可
using UnityEngine;
using DG.Tweening;

public class CannonBattlePosture : EnemyBattlePosture
{
    Transform muzzlePos = null;
    float duration = 0.5f;

    protected override void OnEnter(StateMachine<EnemyBase>.State prevState)
    {
        base.OnEnter(prevState);
        if (IsActive && Owner.Player.Value && muzzlePos)
        {
            Debug.Log("ロックオン");
            muzzlePos.DOLookAt(Owner.Player.Value.transform.position, duration);
        }
    }

    protected override void OnExit(StateMachine<EnemyBase>.State nextState)
    {
        base.OnExit(nextState);
        if(nextState is EnemySearch)
        {
            muzzlePos.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log(muzzlePos.rotation);
        }
    }
    protected override void OnBattlePosture()
    {
        base.OnBattlePosture();
        muzzlePos.rotation = Quaternion.LookRotation(Owner.Player.Value.transform.position - muzzlePos.position, Vector3.right);
    }

    protected override void OnSetUpState()
    {
        base.OnSetUpState();
        if (Owner is CannonEnemy cannonEnemy)
        {
            muzzlePos = cannonEnemy.MuzzlePos;
        }
    }
}
