//日本語コメント可
using UnityEngine;

public class CannonEnemy : EnemyBase
{
    [SerializeField,Header("砲身のTransform")] Transform _muzzlePos = null;

    public Transform MuzzlePos => _muzzlePos;

    protected override void SetActionState()
    {
        //戦闘態勢解除
        _stateMachine.AddTransition<CannonBattlePosture, CannonSearch>((int)StateOfEnemy.Saerching);
        //プレイヤーへの攻撃
        _stateMachine.AddTransition<CannonBattlePosture, FireCannon>((int)StateOfEnemy.NormalAttack);
        //戦闘態勢の遷移
        _stateMachine.AddAnyTransition<CannonBattlePosture>((int)StateOfEnemy.BattlePosture);
        //死亡
        _stateMachine.AddAnyTransition<EnemyDeath>((int)StateOfEnemy.Death);
    }
}
