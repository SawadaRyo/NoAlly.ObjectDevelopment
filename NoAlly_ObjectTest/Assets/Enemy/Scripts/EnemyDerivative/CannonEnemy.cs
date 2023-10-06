//日本語コメント可
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : EnemyBase
{
    protected override void SetActionState()
    {
        base.SetActionState();
        //プレイヤーへの攻撃
        _stateMachine.AddTransition<EnemyBattlePosture, FireCannon>((int)StateOfEnemy.NormalAttack);
        _stateMachine.AddTransition<FireCannon,EnemyBattlePosture > ((int)StateOfEnemy.BattlePosture);
    }
}
