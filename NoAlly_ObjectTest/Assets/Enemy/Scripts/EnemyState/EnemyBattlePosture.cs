//日本語コメント可
using UniRx;
using UnityEngine;
using State = StateMachine<EnemyBase>.State;

public class EnemyBattlePosture : State
{
    Interval currentInterval = null;

    protected virtual void OnBattlePosture() { }

    protected override void OnLateEntar(State prevState)
    {
        base.OnEnter(prevState);
        if (Owner.Player.Value == null)
        {
            Debug.Log("攻撃中止");
            Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        OnBattlePosture();
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
        currentInterval = new(Owner.Paramater<EnemyParamaterBase>()._attackInterval);
        Owner.Player
            .Skip(1)
            .Where(player => player == null && IsActive)
            .Subscribe(player =>
            {
                Debug.Log("攻撃中止2");
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
                Owner.ObjectAnimator.SetBool("InSight", false);
            }).AddTo(Owner);
    }
}
