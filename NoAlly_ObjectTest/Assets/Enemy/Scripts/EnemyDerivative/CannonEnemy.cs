//日本語コメント可
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : EnemyBase
{
    protected override void SetActionState()
    {
        //戦闘態勢解除
        _stateMachine.AddTransition<EnemyBattlePosture, CannonSearch>((int)StateOfEnemy.Saerching);
        //プレイヤーへの攻撃
        _stateMachine.AddTransition<EnemyBattlePosture, FireCannon>((int)StateOfEnemy.NormalAttack);
        //戦闘態勢の遷移
        _stateMachine.AddAnyTransition<EnemyBattlePosture>((int)StateOfEnemy.BattlePosture);
        //死亡
        _stateMachine.AddAnyTransition<EnemyDeath>((int)StateOfEnemy.Death);
    }
}
