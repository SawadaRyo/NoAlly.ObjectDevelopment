//日本語コメント可
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputParamater", menuName = "ScriptableObjects/PlayerInputParamater", order = 1)]
[System.Serializable]

public class PlayerInputParamater : ScriptableObject
{
    [Header("Power")]
    [SerializeField, Header("移動速度")]
    float _speed = 5f;
    [SerializeField, Header("ダッシュ速度")]
    float _dashSpeed = 7f;
    [SerializeField, Header("ジャンプ力")]
    float _jumpPower = 5f;

    [Header("Interval")]

    [SerializeField, Header("溜め攻撃の溜め時間")]
    float _chargeInterval = 1f;

    [Header("GroundJudge")]

    [SerializeField, Header("接地判定のRayの射程")]
    float _graundDistance = 1f;
    [SerializeField, Header("接地判定のSphierCastの半径")]
    float _isGroundRengeRadios = 1f;
    [SerializeField, Header("接地判定のLayerMask")]
    LayerMask _groundMask = ~0;

    [Header("Wall")]

    [SerializeField, Header("プレイヤーの壁キックの力")]
    float _wallJump = 7f;
    [SerializeField, Header("壁をずり落ちる速度")]
    float _wallSlideSpeed = 0.8f;
    [SerializeField, Header("壁の接触判定のRayの射程")]
    float _walldistance = 0.1f;
    [SerializeField, Header("壁の接触判定")]
    LayerMask[] _wallMask;

    public float speed => _speed;
    public float dashSpeed => _dashSpeed;
    public float jumpPower => _jumpPower;
    public float chargeInterval => _chargeInterval;
    public float graundDistance => _graundDistance;
    public float isGroundRengeRadios => _isGroundRengeRadios;
    public LayerMask groundMask => _groundMask;
    public float wallJump => _wallJump;
    public float wallSlideSpeed => _wallSlideSpeed;
    public float walldistance => _walldistance;
    public LayerMask[] wallMask => _wallMask;

}
