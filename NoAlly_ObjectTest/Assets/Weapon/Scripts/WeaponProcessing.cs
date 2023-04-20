using System;
using UnityEngine;
using UniRx;

/// <summary>
/// 武器のモーションや可視化などの武器にまつわる処理を行うクラス
/// </summary>
public class WeaponProcessing : MonoBehaviour
{
    [SerializeField, Header("武器のアニメーター")]
    Animator _weaponAnimator = null;


    [Tooltip("全武器のデータ")]
    WeaponDatas[] _weaponDatas = new WeaponDatas[Enum.GetValues(typeof(WeaponType)).Length];
    [Tooltip("メイン武器とサブ武器")]
    WeaponDatas[] _mainAndSub = new WeaponDatas[2];

    [Tooltip("装備している武器")]
    WeaponDatas _targetWeapon;

    float time = 0;

    BoolReactiveProperty _isSwichWeapon = new BoolReactiveProperty();

    public WeaponDatas TargetWeapon { get => _targetWeapon; set => _targetWeapon = value; }
    public IReadOnlyReactiveProperty<bool> IsSwichWeapon => _isSwichWeapon;



    /// <summary>
    /// メイン武器・サブ武器の装備をボタンで切り替える関数
    /// </summary>
    public void SwichWeapon(bool weaponSwitch)
    {
        if (!weaponSwitch)
        {
            _targetWeapon = _mainAndSub[(int)CommandType.MAINWEAPON];
        }
        else
        {
            _targetWeapon = _mainAndSub[(int)CommandType.SUBWEAPON];
        }
    }
    /// <summary>
    /// 武器の装備
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="type"></param>
    public void SetEquipment(WeaponType weaponType, CommandType type)
    {
        _mainAndSub[(int)type] = _weaponDatas[(int)weaponType];
        _weaponAnimator.SetInteger("WeaponType", (int)weaponType);
    }
    public void SetElement(ElementType elementType)
    {
        switch(elementType)
        {
            case ElementType.RIGIT:
                _weaponAnimator.SetBool("IsOpen", false);
                break;
            default:
                _weaponAnimator.SetBool("IsOpen", true);
                break;
        }
    }
}





