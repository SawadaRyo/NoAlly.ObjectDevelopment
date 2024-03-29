using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ステートマシンに必要な要素
//1:ステートのリスト管理
//2:ステート遷移開始、遷移中、遷移完了の検知

//ステートに必要な要素
//1:ステートの重要度
//2:ステート時の処理の内容
//3:ステート遷移開始、遷移中、遷移完了それぞれの処理
//4:自身から遷移できるステートの保持

[Serializable]
/// <summary>
/// ステートマシン
/// </summary>
public class StateMachine<TOwner>
{
    /// <summary>
    /// ステートを表すクラス
    /// </summary>
    public abstract class State
    {

        [Tooltip("このステートを管理しているステートマシン")]
        protected StateMachine<TOwner> StateMachine => stateMachine;
        internal StateMachine<TOwner> stateMachine;
        [Tooltip("遷移の一覧")]
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();
        [Tooltip("このステートの起動判定")]
        bool isActive = false;
        /// <summary>
        /// このステートのオーナー
        /// </summary>
        protected TOwner Owner => stateMachine.Owner;
        /// <summary>
        /// このステートの起動判定
        /// </summary>
        protected bool IsActive => isActive;

        /// <summary>
        /// ステート条件指定
        /// </summary>
        internal void SetUpState()
        {
            OnSetUpState();
        }

        /// <summary>
        /// 遷移条件
        /// </summary>
        protected virtual void OnSetUpState() { }

        /// <summary>
        /// ステート開始(Enterよりも早く呼ばれる)
        /// </summary>
        internal void EarlyEntar(State prevState)
        {
            OnEarlyEntar(prevState);
        }

        protected virtual void OnEarlyEntar(State prevState) { }

        /// <summary>
        /// ステート開始
        /// </summary>
        internal void Enter(State prevState)
        {
            isActive = true;
            OnEnter(prevState);
        }
        /// <summary>
        /// ステートを開始した時に呼ばれる
        /// </summary>
        protected virtual void OnEnter(State prevState) { }

        internal void LateEntar(State prevState)
        {
            OnLateEntar(prevState);
        }

        protected virtual void OnLateEntar(State prevState) { }

        /// <summary>
        /// ステート更新
        /// </summary>
        internal void Update()
        {
            OnUpdate();
        }
        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// ステート終了
        /// </summary>
        internal void Exit(State nextState)
        {
            isActive = false;
            OnExit(nextState);
        }
        /// <summary>
        /// ステートを終了した時に呼ばれる
        /// </summary>
        protected virtual void OnExit(State nextState) { }
    }

    /// <summary>
    /// どのステートからでも特定のステートへ遷移できるようにするための仮想ステート
    /// </summary>
    public sealed class AnyState : State { }

    /// <summary>
    /// このステートマシンのオーナー
    /// </summary>
    public TOwner Owner { get; }
    /// <summary>
    /// 現在のステート
    /// </summary>
    public State CurrentState { get; private set; }

    // ステートリスト
    private LinkedList<State> _states = new LinkedList<State>();

    /// <summary>
    /// ステートマシンを初期化する
    /// </summary>
    /// <param name="owner">ステートマシンのオーナー</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// ステートを追加する（ジェネリック版）
    /// </summary>
    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        _states.AddLast(state);
        return state;
    }

    /// <summary>
    /// 特定のステートを取得、なければ生成する
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
    /// 遷移を定義する
    /// </summary>
    /// <param name="eventId">イベントID</param>
    public void AddTransition<TFrom, TTo>(int eventId)
        where TFrom : State, new()
        where TTo : State, new()
    {
        var from = GetOrAddState<TFrom>();
        if (from.transitions.ContainsKey(eventId))
        {
            // 同じイベントIDの遷移を定義済
            throw new System.ArgumentException(
                $"ステート'{nameof(TFrom)}'に対してイベントID'{eventId.ToString()}'の遷移は定義済です");
        }

        var to = GetOrAddState<TTo>();
        from.transitions.Add(eventId, to);
    }

    /// <summary>
    /// どのステートからでも特定のステートへ遷移できるイベントを追加する
    /// </summary>
    /// <param name="eventId">イベントID</param>
    public void AddAnyTransition<TTo>(int eventId) where TTo : State, new()
    {
        AddTransition<AnyState, TTo>(eventId);
    }

    /// <summary>
    /// ステートマシンの実行を開始する（ジェネリック版）
    /// </summary>
    public void Start<TFirst>() where TFirst : State, new()
    {
        Start(GetOrAddState<TFirst>());
    }

    /// <summary>
    /// ステートマシンの実行を開始する
    /// </summary>
    /// <param name="firstState">起動時のステート</param>
    /// <param name="param">パラメータ</param>
    public void Start(State firstState)
    {
        CurrentState = firstState;
        CurrentState.Enter(null);
        _states.ToList().ForEach(state => state.SetUpState());
    }

    /// <summary>
    /// ステートを更新する
    /// </summary>
    public void Update()
    {
        CurrentState.Update();
    }

    /// <summary>
    /// イベントを発行する
    /// </summary>
    /// <param name="eventId">イベントID</param>
    public void Dispatch(int eventId)
    {
        State to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // イベントに対応する遷移が見つからなかった
                return;
            }
        }
        Change(to);
    }

    /// <summary>
    /// ステートを変更する ステートの呼ばれる関数の順番は下記の通り <br /> 
    /// 遷移先のステートのEarlyEntar() → 遷移前のExit() → 遷移後のEntar() → 遷移後のLateEntar()
    /// </summary>
    /// <param name="nextState">遷移先のステート</param>
    private void Change(State nextState)
    {
        nextState.EarlyEntar(nextState);
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
        CurrentState.LateEntar(nextState);
    }
}
