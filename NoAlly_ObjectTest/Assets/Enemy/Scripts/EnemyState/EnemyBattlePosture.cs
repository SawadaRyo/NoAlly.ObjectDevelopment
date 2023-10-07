//日本語コメント可
using UniRx;
using UnityEngine;
using State = StateMachine<EnemyBase>.State;

public class EnemyBattlePosture : State
{
    Interval currentInterval = null;
    protected override void OnEnter(State prevState)
    {
        base.OnEnter(prevState);
        Owner.ObjectAnimator.SetBool("InSight", true);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (currentInterval.IsCountUp())
        {
            Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.NormalAttack);
            Debug.Log("攻撃だよ！");
        }
        if (Owner.Player.Value == null)
        {
            Debug.Log("攻撃中止");
            Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
        }
    }

    protected override void OnExit(State nextState)
    {
        base.OnExit(nextState);
        currentInterval.ResetTimer();
    }

    protected override void OnSetUpState()
    {
        currentInterval = new(Owner.Paramater._attackInterval);
        Owner.Player
            .Skip(1)
            .Where(player => player == null && IsActive)
            .Subscribe(player =>
            {
                Debug.Log("攻撃中止2");
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
            }).AddTo(Owner);
    }
}
