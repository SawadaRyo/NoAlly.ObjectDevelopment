using System;
using UnityEngine;
using UniRx;

public class EnemyBase : ObjectBase, IObjectPool<IObjectGenerator>
{
    [SerializeField, Header("エネミーの基本データ")]
    protected EnemyParamaterBase _enemyParamater = null;
    [SerializeField, Header("索敵範囲の中心")]
    protected Transform _center = null;
    [SerializeField, Header("エネミーのRigidbody")]
    protected Rigidbody _rb = null;

    [Tooltip("ステートマシン")]
    protected StateMachine<EnemyBase> _stateMachine = null;
    [Tooltip("")]
    protected IObjectGenerator _owner = null;
    [Tooltip("プレイヤーのステータスクラス")]
    ReactiveProperty<StatusBase> _playerStatus = null;
    [Tooltip("エネミーの攻撃方法を保存する\r\n 基本的にアニメーションイベントから内部を変更する。使用するのは先頭の番号が「4」から始まる列挙")]
    ReactiveProperty<StateOfEnemy> _attackEnum = null;

    public EnemyParamaterBase Paramater => _enemyParamater;
    /// <summary>
    /// ステートマシーンのオーナー(自分)を返すプロパティ(読み取り専用)
    /// </summary>
    public StateMachine<EnemyBase> EnemyStateMachine => _stateMachine;
    /// <summary>
    /// プレイヤーのステータスデータのプロパティ(読み取り専用)
    /// </summary>
    public IReadOnlyReactiveProperty<StatusBase> Player => _playerStatus;
    /// <summary>
    /// エネミーの攻撃状況(読み取り専用)
    /// </summary>
    public IReadOnlyReactiveProperty<StateOfEnemy> EnemyAttackEnum => _attackEnum;
    /// <summary>
    /// このオブジェクトの生成主(読み取り専用)
    /// </summary>
    public IObjectGenerator Owner => _owner;

    public void AttackEnum(StateOfEnemy attackEnum)
    {
        _attackEnum.SetValueAndForceNotify(attackEnum);
    }

    /// <summary>
    /// 索敵範囲内にプレイヤーが存在するかを判定する処理
    /// </summary>
    /// <returns></returns>
    public StatusBase InSight()
    {
        Collider[] inSight = Physics.OverlapSphere(_center.position, _enemyParamater.searchRenge, _enemyParamater.targetLayer);
        foreach (var s in inSight)
        {
            if (s.gameObject.TryGetComponent(out StatusBase player))
            {
                return player;
            }
        }
        return null;
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    void OnUpdate()
    {
        Observable.EveryFixedUpdate()
            .Where(_ => (_stateMachine != null && _isActive))
            .Subscribe(_ =>
            {
                _playerStatus.Value = InSight();
                _stateMachine.Update();
            }).AddTo(this);
    }
    /// <summary>
    /// エネミーのステート設定処理
    /// </summary>
    protected virtual void SetActionState()
    {
        //戦闘態勢解除
        _stateMachine.AddTransition<EnemyBattlePosture, EnemySearch>((int)StateOfEnemy.Saerching);
        //戦闘態勢
        _stateMachine.AddTransition<EnemySearch, EnemyBattlePosture>((int)StateOfEnemy.BattlePosture);
        //死亡
        _stateMachine.AddAnyTransition<EnemyDeath>((int)StateOfEnemy.Death);
    }
    /// <summary>
    /// オブジェクト有効時に呼ぶ関数
    /// </summary>
    public virtual void Create()
    {
        _isActive = true;
        ActiveCollider(true);
        _objectAnimator.SetBool("Death", false);
    }
    /// <summary>
    /// オブジェクト非有効時に呼ぶ関数
    /// </summary>
    public virtual void Disactive()
    {
        _isActive = false;
        ActiveObject(false);
        _objectAnimator.SetBool("Death", true);

    }
    /// <summary>
    /// オブジェクト非有効時に呼ぶ関数(インターバル有)
    /// </summary>
    public virtual void Disactive(float interval) { }
    /// <summary>
    /// オブジェクト生成時に呼ぶ関数
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <param name="owner"></param>
    public virtual void DisactiveForInstantiate(IObjectGenerator owner = null, bool isActive = false)
    {
        if (owner != null)
        {
            _owner = owner;
        }
        _playerStatus = new();
        _attackEnum = new(StateOfEnemy.None);
        _stateMachine = new StateMachine<EnemyBase>(this);
        SetActionState();
        //ステート開始
        _stateMachine.Start<EnemySearch>();
        OnUpdate();
        if (isActive)
        {
            Create();
        }
        else
        {
            Disactive();
        }
    }
    /// <summary>
    /// オブジェクトを破棄する際に呼ばれる関数
    /// </summary>
    public virtual void Destroy()
    {
        _attackEnum.Dispose();
        _playerStatus.Dispose();
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_center.position, _enemyParamater.searchRenge);
    }
#endif
}

