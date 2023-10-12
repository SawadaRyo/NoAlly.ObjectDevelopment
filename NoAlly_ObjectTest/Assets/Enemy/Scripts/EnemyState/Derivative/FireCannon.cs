using UnityEngine;

public class FireCannon : EnemyNormalAttack
{
    protected override void OnSetUpState()
    {
        _attackAnimationName = "TestEnemyAttack";
        base.OnSetUpState();
    }
    protected override void AttackBehaviour(StateOfEnemy attackEnum)
    {
        Debug.Log("Fire");
    }
}
