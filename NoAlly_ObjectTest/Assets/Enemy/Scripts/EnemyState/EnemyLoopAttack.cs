//日本語コメント可
using UniRx;
using UnityEngine;

/// <summary>
/// 敵の継続攻撃ステート\n 攻撃処理はEnemyAttackEnumの値がNone以外になった時実行し、Noneになったら処理を停止する
/// </summary>
public class EnemyLoopAttack : EnemyAttack
{
    protected override void OnSetUpState()
    {
        base.OnSetUpState();
        Owner.EnemyAttackEnum
            .Skip(1)
            .Where(attack =>
            {
                if (!IsActive) return false;

                //列挙が攻撃関係かNoneだった場合true
                int numberOfDigits = (attack == 0) ? 1 : ((int)Mathf.Log10((int)attack) + 1) - 1;
                int digits = (int)Mathf.Pow(10, numberOfDigits);
                int result = (int)attack / digits;
                return ((result == (int)StateOfEnemy.Attack) || attack == StateOfEnemy.None) ? true : false;
            })
            .Subscribe(attack =>
            {
                AttackBehaviour(attack);
            }).AddTo(Owner);
    }
}
