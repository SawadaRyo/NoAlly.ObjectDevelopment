//日本語コメント可
using UniRx;
using UnityEngine;

/// <summary>
/// 敵の通常攻撃ステート\n 攻撃処理はEnemyAttackEnumの値がNone以外になった時実行する
/// </summary>
public class EnemyNormalAttack : EnemyAttack
{
    protected override void OnSetUpState()
    {
        Debug.Log("Subscribed");
        base.OnSetUpState();
        Owner.EnemyAttackEnum
            .Skip(1)
            .Where(attack =>
            {
                if(!IsActive) return false;

                //列挙が攻撃関係だった場合true
                int numberOfDigits = (attack == 0) ? 1 : ((int)Mathf.Log10((int)attack) + 1) - 1;
                int digits = (int)Mathf.Pow(10, numberOfDigits);
                int result = (int)attack / digits;
                return (result == (int)StateOfEnemy.Attack) ? true : false;
            })
            .Subscribe(attack =>
            {
                AttackBehaviour(attack);
            }).AddTo(Owner);
    }
}
