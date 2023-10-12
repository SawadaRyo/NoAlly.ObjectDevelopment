using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StatusBase : MonoBehaviour
{
    [SerializeField, Tooltip("HP�̏��")]
    protected float _maxHP = 5;
    [SerializeField, Tooltip("���G���Ԃ̒l")]
    protected float _invincibleTimeValue = 1f;


    [Tooltip("�I�u�W�F�N�g��HP")]
    protected float _hp = 0;
    [Tooltip("�I�u�W�F�N�g�̐�������")]
    protected bool _living = true;
    [Tooltip("Animator�̊i�[�ϐ�")]
    protected Animator _animator;
    [Tooltip("AudioSource���i�[����ϐ�")]
    protected AudioSource _audioSource = null;
    [Tooltip("���G����")]
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
            Debug.LogError("�����蔻�肪����܂���");
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
