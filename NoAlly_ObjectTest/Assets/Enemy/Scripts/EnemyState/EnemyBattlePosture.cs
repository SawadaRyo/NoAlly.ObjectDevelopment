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
        if (Owner.Player.Value == null)
        {
            Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
        }
        else
        {
            Owner.ObjectAnimator.SetBool("InSight", true);
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (currentInterval.IsCountUp())
        {
            Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.NormalAttack);
            Debug.Log("攻撃だよ！");
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
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
            }).AddTo(Owner);
    }
}
