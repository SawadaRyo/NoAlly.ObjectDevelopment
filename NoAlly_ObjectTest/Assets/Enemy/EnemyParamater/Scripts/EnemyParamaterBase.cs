//日本語コメント可
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParamater", menuName = "ScriptableObjects/ActorParamater/Enemy", order = 1)]
public class EnemyParamaterBase : ScriptableObject
{
    [SerializeField, Header("索敵範囲の有効範囲")]
    public float searchRenge = 0f;
    [SerializeField, Header("索敵用のレイヤー")]
    public LayerMask targetLayer = ~0;

    [Space(15)]

    [SerializeField, Header("エネミーの体力")]
    public float hp = 5f;
    [SerializeField, Header("エネミーの攻撃力")]
    public ObjectDefenceValue[] enemyPowers = new ObjectDefenceValue[4];

    [Space(15)]

    [SerializeField, Header("エネミーの移動速度")]
    public float speed = 3f;
    [SerializeField, Range(1f, 5f), Header("エネミーの攻撃速度")]
    public float _attackInterval = 1f;
}
