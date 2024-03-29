using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StatusBase : MonoBehaviour
{
    [SerializeField, Tooltip("HPの上限")]
    protected float _maxHP = 5;
    [SerializeField, Tooltip("無敵時間の値")]
    protected float _invincibleTimeValue = 1f;


    [Tooltip("オブジェクトのHP")]
    protected float _hp = 0;
    [Tooltip("オブジェクトの生死判定")]
    protected bool _living = true;
    [Tooltip("Animatorの格納変数")]
    protected Animator _animator;
    [Tooltip("AudioSourceを格納する変数")]
    protected AudioSource _audioSource = null;
    [Tooltip("無敵時間")]
    protected Interval _invincibleTime = null;
    bool _hitable = true;

    public bool Living => _living;

    public virtual void Damage(ObjectPowerValue damageValue, ElementType type)
    {
        _hp -= DamageCalculation(damageValue, type);
    }
    public void ChangeParamater(float value,bool isPlus)
    {

    }
    public virtual void Death()
    {
        if (_hp <= 0)
        {
            Debug.Log("Death");
        }
    }

    public virtual void Initialize()
    {
        _living = true;
        _invincibleTime = new Interval(_invincibleTimeValue);
        _animator = GetComponent<Animator>();
        if (!GetComponentInChildren<HitParameter>())
        {
            Debug.LogError("当たり判定がありません");
        }
    }

    protected float DamageCalculation(ObjectPowerValue damageValue, ElementType type)
    {
        if (_hitable)
        {
            StartCoroutine(StartCountDown());
            return damageValue.defaultPower + damageValue.elementPower;
        }
        return 0f;
    }
    public IEnumerator StartCountDown()
    {
        _hitable = false;
        while (true)
        {
            if (_invincibleTime.IsCountUp())
            {
                _hitable = true;
                _invincibleTime.ResetTimer();
                break;
            }
            yield return null;
        }
    }
}
