//日本語コメント可
using UnityEngine;
using StateChacker;
using State = StateMachine<EnemyBase>.State;

/// <summary>
/// 敵の攻撃ステート
/// </summary>
public abstract class EnemyAttack : State
{
    [Tooltip("再生するアニメーション名")]
    protected string _attackAnimationName = "";

    /// <summary>
    /// 攻撃の挙動を処理する　例：エネミーが弾を生成し発射する処理を実行する
    /// </summary>
    protected virtual void AttackBehaviour(StateOfEnemy attackEnum) { }

    protected override void OnEnter(State prevState)
    {
        base.OnEnter(prevState);
        //攻撃のアニメーションを再生する　例：エネミーの弾を撃つアニメーションを再生する
        Owner.ObjectAnimator.Play(_attackAnimationName);
    }

    protected override void OnSetUpState()
    {
        base.OnSetUpState();
        if(Owner.ObjectAnimator != null)
        {
            AnimationStateChacker.Instance
            .StateChacker(Owner.ObjectAnimator, _attackAnimationName, ObservableType.SteteExit, () =>
            {
                Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.BattlePosture);
            });
        }
    }
}
