//日本語コメント可
using UniRx;
using State = StateMachine<EnemyBase>.State;

public class EnemyBattlePosture : State
{
    /// <summary>
    /// 攻撃のアニメーションを再生する　例：エネミーの弾を撃つアニメーションを再生する
    /// </summary>
    protected virtual void PlayAttackAnimation() { }
    /// <summary>
    /// 攻撃の挙動を処理する　例：エネミーが弾を生成し発射する処理を実行する
    /// </summary>
    protected virtual void AttackBehaviour(EnemyAttackEnum attackEnum) { }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        PlayAttackAnimation();
    }

    protected override void OnTranstion()
    {
        Owner.Player
            .Where(_ => IsActive == true)
            .Subscribe(player =>
            {
                if (player == null)
                {
                    Owner.EnemyStateMachine.Dispatch((int)StateOfEnemy.Saerching);
                    Owner.ObjectAnimator.SetBool("InSight", false);
                }
            }).AddTo(Owner);
        Owner.EnemyAttackEnum
            .Subscribe(attack =>
            {

            }).AddTo(Owner);
    }
}
