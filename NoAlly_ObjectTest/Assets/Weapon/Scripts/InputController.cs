//日本語コメント可
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public enum PlayerActionState
{
    None,
    Upper,
    Down,
    Move,
    Jump,

    Attack,
    Attacking,
    Charging,
    ChargingAttack,

    Damaged,
    Damaging
}
public enum PlayerStateFlag
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
    [Tooltip("")]
    PlayerMove _playerMove = null;
    [Tooltip("")]
    PlayerAction _playerAction = null;
    [Tooltip("Animationの遷移状況")]
    ObservableStateMachineTrigger _trigger = default;
    [Tooltip("")]
    PlayerActionState _attackState = PlayerActionState.None;

    public ObservableStateMachineTrigger Trigger => _trigger;
    public PlayerActionState AttackState => _attackState;

    void Start()
    {
        _playerMove = new(this);
        _playerAction = new(this);
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
        if (IsMoveJudge(moveVec) != PlayerActionState.None)
        {
            _playerMove.Move(moveVec.x, moveVec.y, _inputParamater.speed, _rb);
        }
        if (_attackState != PlayerActionState.None)
        {
            _playerAction.Attack(_animator,, _attackState);
        }
    }

    PlayerActionState IsMoveJudge(Vector2 moveVec)
    {
        if (Mathf.Abs(moveVec.x) > 0)
        {
            return PlayerActionState.Move;
        }
        else if (moveVec.y > 0)
        {
            return PlayerActionState.Upper;
        }
        else if (moveVec.y < 0)
        {
            return PlayerActionState.Down;
        }
        return PlayerActionState.None;
    }

    void ActionFlag()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _attackState = PlayerActionState.Attack;
        }
        else if (Input.GetButton("Attack"))
        {
            if (_chargeTime < _inputParamater.chargeInterval)
            {
                _chargeTime += Time.deltaTime;
            }
            if (_chargeTime >= _inputParamater.chargeInterval)
            {
                _attackState = PlayerActionState.Charging;
            }
        }
        else if (Input.GetButtonUp("Attack"))
        {
            _chargeTime = 0f;
            _attackState = PlayerActionState.ChargingAttack;
        }
        else
        {
            _attackState = PlayerActionState.None;
        }
    }
}
