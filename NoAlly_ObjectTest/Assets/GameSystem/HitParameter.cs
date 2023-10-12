using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����蔻��ƃ_���[�W�v�Z�̗v�f��n���֐�
/// </summary>

[RequireComponent(typeof(Collider))]
public class HitParameter : MonoBehaviour, IHitBehavorOfAttack
{
    [Header("�U�����ꂽ���̔{���B�l�������قǎ󂯂�_���[�W���オ��")]
    [SerializeField]
    ObjectPowerValue DefensePercentage;
    [Range(0f, 2f), SerializeField, Header("�����U���̔{��")]
    float _rigitDefensePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("���U���̔{��")]
    float _fireDifansePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("�d�C�U���̔{��")]
    float _elekeDifansePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("�X�U���̔{��")]
    float _frozenDifansePercentage = 1f;

    [Tooltip("�I�u�W�F�N�g��StatusBase")]
    StatusBase _status = null;

    /// <summary>
    /// �����U���̔{���̃v���p�e�B(�ǂݎ���p)
    /// </summary>
    public float RigitDefensePercentage => _rigitDefensePercentage;
    /// <summary>
    /// ���U���̔{���̃v���p�e�B(�ǂݎ���p)
    /// </summary>
    public float FireDifansePercentage => _fireDifansePercentage;
    /// <summary>
    /// �d�C�U���̔{���̃v���p�e�B(�ǂݎ���p)
    /// </summary>
    public float ElekeDifansePercentage => _elekeDifansePercentage;
    /// <summary>
    /// �X�U���̔{���̃v���p�e�B(�ǂݎ���p)
    /// </summary>
    public float FrozenDifansePercentage => _frozenDifansePercentage;
    /// <summary>
    /// �I�u�W�F�N�g��StatusBase�̃v���p�e�B(�ǂݎ���p)
    /// </summary>
    StatusBase Status
    {
        get
        {
            if (_status == null)
            {
                _status = this.GetComponentInParent<StatusBase>();
            }
            return _status;
        }
    }

    public void BehaviorOfHit(ObjectPowerValue damageValue, ElementType type)
    {
        float baseDamage = damageValue.defaultPower * _rigitDefensePercentage;
        float elemantDamage = 0;
        switch (type)
        {
            case ElementType.FIRE:
                elemantDamage = damageValue.elementPower * _fireDifansePercentage;
                break;
            case ElementType.ELEKE:
                elemantDamage = damageValue.elementPower * _elekeDifansePercentage;
                break;
            case ElementType.FROZEN:
                elemantDamage = damageValue.elementPower * _frozenDifansePercentage;
                break;
            default:
                break;
        }
        //ObjectPowerValue result = new WeaponPower(baseDamage, elemantDamage);
        //Status.Damage(result, type);
    }

    public void BehaviorOfHit(float[] damageValue, ElementType type)
    {
        throw new System.NotImplementedException();
    }
}
