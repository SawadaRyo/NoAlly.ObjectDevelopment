//日本語コメント可
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public enum ActorState
{
    //初期化
    None,

    //プレイヤーのロケーション
    Ground,
    Air,

    //移動方向
    Right,
    Left,
    Upper,
    Down,

    //移動状態
    Walk,
    Dash,

    //プレイヤーの攻撃遷移状況
    Attack,
    Attacking,
    Charging,
    ChargingAttack,

    //プレイヤーのダメージ
    Damaged,
    Damaging
}
public enum PlayerAbleFlag
{
    None = -1,
    Move = 0,
    Action = 1,
    Damage = 2
}


public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerInputParamater _inputParamater = null;
    [SerializeField]
    Rigidbody _rb = null;
    [SerializeField]
    Animator _animator = null;


    float _chargeTime = 0f;
    [Tooltip("プレイヤーの移動")]
    PlayerMove _playerMove = new();
    [Tooltip("プレイヤーの攻撃")]
    PlayerAction _playerAction = new();
    [Tooltip("Animationの遷移状況")]
    ObservableStateMachineTrigger _trigger = default;
    [Tooltip("")]
    ActorState _actorAttackState = ActorState.None;
    [Tooltip("")]
    ActorState _actorMoveState = ActorState.None;

    public ObservableStateMachineTrigger Trigger => _trigger;
    public ActorState ActorAttackState => _actorAttackState;

    void Start()
    {
        _trigger = _animator.GetBehaviour<ObservableStateMachineTrigger>();
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                UpdateToInput();
            }).AddTo(this);
    }
    void UpdateToInput()
    {
        Vector2 moveVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vartical"));
        bool isDash = Input.GetButton("Dash");
        if (IsMoveJudge(isDash,moveVec) != (ActorState.None,ActorState.None))
        {
            _playerMove.Move(moveVec.x, moveVec.y, _inputParamater.speed, _rb,_animator,_actorMoveState);
        }
        if (_actorAttackState != ActorState.None)
        {
            //_playerAction.Attack(_animator,, _actorAttackState);
        }
    }

    (ActorState,ActorState) IsMoveJudge(bool isDash, Vector2 moveVec)
    {
        ActorState actorMoveVec = ActorState.None;
        ActorState actorDashCheck = ActorState.None;
      
        if (moveVec.x > 0)
        {
            actorMoveVec = ActorState.Right;
        }
        else if (moveVec.x < 0)
        {
            actorMoveVec = ActorState.Left;
        }
        else if (moveVec.y > 0)
        {
            actorMoveVec = ActorState.Upper;
        }
        else if (moveVec.y < 0)
        {
            actorMoveVec = ActorState.Down;
        }

        if(isDash)
        {
            actorDashCheck = ActorState.Dash;
        }

        return (actorMoveVec, actorDashCheck);
    }

    void ActionFlag()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _actorAttackState = ActorState.Attack;
        }
        else if (Input.GetButton("Attack"))
        {
            if (_chargeTime < _inputParamater.chargeInterval)
            {
                _chargeTime += Time.deltaTime;
            }
            if (_chargeTime >= _inputParamater.chargeInterval)
            {
                _actorAttackState = ActorState.Charging;
            }
        }
        else if (Input.GetButtonUp("Attack"))
        {
            if (_actorAttackState == ActorState.Charging)
            {
                _actorAttackState = ActorState.ChargingAttack;
            }
            _chargeTime = 0f;
        }
        else
        {
            _actorAttackState = ActorState.None;
        }
    }
}
