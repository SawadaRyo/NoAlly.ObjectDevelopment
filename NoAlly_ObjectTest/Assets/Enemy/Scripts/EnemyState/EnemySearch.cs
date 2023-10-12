using UnityEngine;
using DG.Tweening;
using UniRx;
using State = StateMachine<EnemyBase>.State;

public class EnemySearch : State
{
    protected virtual void SearchBehaviour() { }

    protected override void OnEnter(State prevState)
    {
        base.OnEnter(prevState);
        Owner.ObjectAnimator.SetBool("InSight", false);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        SearchBehaviour();
    }

    protected override void OnSetUpState()
    {
        base.OnSetUpState();
        Owner.Player
            .Where(player => player != null && IsActive)
            .Subscribe(player =>
            {
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.BattlePosture);
                Owner.ObjectAnimator.SetBool("InSight", true);
            }).AddTo(Owner);
    }
}
