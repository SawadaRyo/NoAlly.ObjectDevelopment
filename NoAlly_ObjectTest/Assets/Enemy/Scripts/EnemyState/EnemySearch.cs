using UnityEngine;
using DG.Tweening;
using UniRx;
using State = StateMachine<EnemyBase>.State;

public class EnemySearch : State
{
    bool _isRotatedLeft;
    float _time = 0;
    float _intervalRotate = 3;
    float _turnDuration = 0.5f;
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
        EnemyRotate();
    }

    protected void EnemyRotate()
    {
        _time += Time.deltaTime;
        if (_time >= _intervalRotate)
        {
            _isRotatedLeft = !_isRotatedLeft;
            if (_isRotatedLeft)
            {
                Owner.transform.DORotate(new Vector3(0f, 0, 0f), _turnDuration);
            }
            else
            {
                Owner.transform.DORotate(new Vector3(0f, 180f, 0f), _turnDuration);
            }
            _time = 0;
        }
    }

    protected override void OnSetUpState()
    {
        base.OnSetUpState();
        Owner.Player
            .Where(player => player != null && IsActive)
            .Subscribe(player =>
            {
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.BattlePosture);
            }).AddTo(Owner);
    }
}
