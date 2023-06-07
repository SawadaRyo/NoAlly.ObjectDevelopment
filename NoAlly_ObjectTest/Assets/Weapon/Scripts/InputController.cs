//日本語コメント可
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public enum PlayerActionState
{
    None,
    Move,
    Jump,
    Attack,
    Attacking,
    Damaged,
    Damaging
}
public enum PlayerState : int
{
    Move,
    Action,
    Damage
}


public class InputController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5f;
    [SerializeField]
    Rigidbody _rb = null;
    [SerializeField]
    Animator _animator = null;

    [Tooltip("")]
    PlayerMove _playerMove = null;
    [Tooltip("Animationの遷移状況")]
    ObservableStateMachineTrigger _trigger = default;
    [Tooltip("")]
    PlayerActionState _state = PlayerActionState.None;

    public ObservableStateMachineTrigger Trigger => _trigger;
    void Start()
    {
        _playerMove = new(this);
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
        if (moveVec != Vector2.zero)
        {
            _playerMove.Move(moveVec.x, moveVec.y, _speed, _rb);
        }

        //PlayerAction.Attack(_animator,, _state);
    }
}
