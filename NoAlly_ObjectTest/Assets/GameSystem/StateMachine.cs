using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�X�e�[�g�}�V���ɕK�v�ȗv�f
//1:�X�e�[�g�̃��X�g�Ǘ�
//2:�X�e�[�g�J�ڊJ�n�A�J�ڒ��A�J�ڊ����̌��m

//�X�e�[�g�ɕK�v�ȗv�f
//1:�X�e�[�g�̏d�v�x
//2:�X�e�[�g���̏����̓��e
//3:�X�e�[�g�J�ڊJ�n�A�J�ڒ��A�J�ڊ������ꂼ��̏���
//4:���g����J�ڂł���X�e�[�g�̕ێ�

[Serializable]
/// <summary>
/// �X�e�[�g�}�V��
/// </summary>
public class StateMachine<TOwner>
{
    /// <summary>
    /// �X�e�[�g��\���N���X
    /// </summary>
    public abstract class State
    {

        [Tooltip("���̃X�e�[�g���Ǘ����Ă���X�e�[�g�}�V��")]
        protected StateMachine<TOwner> StateMachine => stateMachine;
        internal StateMachine<TOwner> stateMachine;
        [Tooltip("�J�ڂ̈ꗗ")]
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();
        [Tooltip("���̃X�e�[�g�̋N������")]
        bool isActive = false;
        /// <summary>
        /// ���̃X�e�[�g�̃I�[�i�[
        /// </summary>
        protected TOwner Owner => stateMachine.Owner;
        /// <summary>
        /// ���̃X�e�[�g�̋N������
        /// </summary>
        protected bool IsActive => isActive;

        /// <summary>
        /// �X�e�[�g�����w��
        /// </summary>
        internal void SetUpState()
        {
            OnSetUpState();
        }

        /// <summary>
        /// �J�ڏ���
        /// </summary>
        protected virtual void OnSetUpState() { }

        /// <summary>
        /// �X�e�[�g�J�n(Enter���������Ă΂��)
        /// </summary>
        internal void EarlyEntar(State prevState)
        {
            OnEarlyEntar(prevState);
        }

        protected virtual void OnEarlyEntar(State prevState) { }

        /// <summary>
        /// �X�e�[�g�J�n
        /// </summary>
        internal void Enter(State prevState)
        {
            isActive = true;
            OnEnter(prevState);
        }
        /// <summary>
        /// �X�e�[�g���J�n�������ɌĂ΂��
        /// </summary>
        protected virtual void OnEnter(State prevState) { }

        internal void LateEntar(State prevState)
        {
            OnLateEntar(prevState);
        }

        protected virtual void OnLateEntar(State prevState) { }

        /// <summary>
        /// �X�e�[�g�X�V
        /// </summary>
        internal void Update()
        {
            OnUpdate();
        }
        /// <summary>
        /// ���t���[���Ă΂��
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// �X�e�[�g�I��
        /// </summary>
        internal void Exit(State nextState)
        {
            isActive = false;
            OnExit(nextState);
        }
        /// <summary>
        /// �X�e�[�g���I���������ɌĂ΂��
        /// </summary>
        protected virtual void OnExit(State nextState) { }
    }

    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���悤�ɂ��邽�߂̉��z�X�e�[�g
    /// </summary>
    public sealed class AnyState : State { }

    /// <summary>
    /// ���̃X�e�[�g�}�V���̃I�[�i�[
    /// </summary>
    public TOwner Owner { get; }
    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    public State CurrentState { get; private set; }

    // �X�e�[�g���X�g
    private LinkedList<State> _states = new LinkedList<State>();

    /// <summary>
    /// �X�e�[�g�}�V��������������
    /// </summary>
    /// <param name="owner">�X�e�[�g�}�V���̃I�[�i�[</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// �X�e�[�g��ǉ�����i�W�F�l���b�N�Łj
    /// </summary>
    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        _states.AddLast(state);
        return state;
    }

    /// <summary>
    /// ����̃X�e�[�g���擾�A�Ȃ���ΐ�������
    /// </summary>
    public T GetOrAddState<T>() where T : State, new()
    {
        foreach (var state in _states)
        {
            if (state is T result)
            {
                return result;
            }
        }
        return Add<T>();
    }

    /// <summary>
    /// �J�ڂ��`����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void AddTransition<TFrom, TTo>(int eventId)
        where TFrom : State, new()
        where TTo : State, new()
    {
        var from = GetOrAddState<TFrom>();
        if (from.transitions.ContainsKey(eventId))
        {
            // �����C�x���gID�̑J�ڂ��`��
            throw new System.ArgumentException(
                $"�X�e�[�g'{nameof(TFrom)}'�ɑ΂��ăC�x���gID'{eventId.ToString()}'�̑J�ڂ͒�`�ςł�");
        }

        var to = GetOrAddState<TTo>();
        from.transitions.Add(eventId, to);
    }

    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���C�x���g��ǉ�����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void AddAnyTransition<TTo>(int eventId) where TTo : State, new()
    {
        AddTransition<AnyState, TTo>(eventId);
    }

    /// <summary>
    /// �X�e�[�g�}�V���̎��s���J�n����i�W�F�l���b�N�Łj
    /// </summary>
    public void Start<TFirst>() where TFirst : State, new()
    {
        Start(GetOrAddState<TFirst>());
    }

    /// <summary>
    /// �X�e�[�g�}�V���̎��s���J�n����
    /// </summary>
    /// <param name="firstState">�N�����̃X�e�[�g</param>
    /// <param name="param">�p�����[�^</param>
    public void Start(State firstState)
    {
        CurrentState = firstState;
        CurrentState.Enter(null);
        _states.ToList().ForEach(state => state.SetUpState());
    }

    /// <summary>
    /// �X�e�[�g���X�V����
    /// </summary>
    public void Update()
    {
        CurrentState.Update();
    }

    /// <summary>
    /// �C�x���g�𔭍s����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void Dispatch(int eventId)
    {
        State to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // �C�x���g�ɑΉ�����J�ڂ�������Ȃ�����
                return;
            }
        }
        Change(to);
    }

    /// <summary>
    /// �X�e�[�g��ύX���� �X�e�[�g�̌Ă΂��֐��̏��Ԃ͉��L�̒ʂ� <br /> 
    /// �J�ڐ�̃X�e�[�g��EarlyEntar() �� �J�ڑO��Exit() �� �J�ڌ��Entar() �� �J�ڌ��LateEntar()
    /// </summary>
    /// <param name="nextState">�J�ڐ�̃X�e�[�g</param>
    private void Change(State nextState)
    {
        nextState.EarlyEntar(nextState);
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
        CurrentState.LateEntar(nextState);
    }
}
