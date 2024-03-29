using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当たり判定とダメージ計算の要素を渡す関数
/// </summary>

[RequireComponent(typeof(Collider))]
public class HitParameter : MonoBehaviour, IHitBehavorOfAttack
{
    [Header("攻撃された時の倍率。値が高いほど受けるダメージが上がる")]
    [SerializeField]
    ObjectPowerValue DefensePercentage;
    [Range(0f, 2f), SerializeField, Header("物理攻撃の倍率")]
    float _rigitDefensePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("炎攻撃の倍率")]
    float _fireDifansePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("電気攻撃の倍率")]
    float _elekeDifansePercentage = 1f;
    [Range(0f, 2f), SerializeField, Header("氷攻撃の倍率")]
    float _frozenDifansePercentage = 1f;

    [Tooltip("オブジェクトのStatusBase")]
    StatusBase _status = null;

    /// <summary>
    /// 物理攻撃の倍率のプロパティ(読み取り専用)
    /// </summary>
    public float RigitDefensePercentage => _rigitDefensePercentage;
    /// <summary>
    /// 炎攻撃の倍率のプロパティ(読み取り専用)
    /// </summary>
    public float FireDifansePercentage => _fireDifansePercentage;
    /// <summary>
    /// 電気攻撃の倍率のプロパティ(読み取り専用)
    /// </summary>
    public float ElekeDifansePercentage => _elekeDifansePercentage;
    /// <summary>
    /// 氷攻撃の倍率のプロパティ(読み取り専用)
    /// </summary>
    public float FrozenDifansePercentage => _frozenDifansePercentage;
    /// <summary>
    /// オブジェクトのStatusBaseのプロパティ(読み取り専用)
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
