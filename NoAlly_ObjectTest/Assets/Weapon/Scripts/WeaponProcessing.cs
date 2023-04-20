using System;
using UnityEngine;
using UniRx;

/// <summary>
/// ����̃��[�V����������Ȃǂ̕���ɂ܂�鏈�����s���N���X
/// </summary>
public class WeaponProcessing : MonoBehaviour
{
    [SerializeField, Header("����̃A�j���[�^�[")]
    Animator _weaponAnimator = null;


    [Tooltip("�S����̃f�[�^")]
    WeaponDatas[] _weaponDatas = new WeaponDatas[Enum.GetValues(typeof(WeaponType)).Length];
    [Tooltip("���C������ƃT�u����")]
    WeaponDatas[] _mainAndSub = new WeaponDatas[2];

    [Tooltip("�������Ă��镐��")]
    WeaponDatas _targetWeapon;

    float time = 0;

    BoolReactiveProperty _isSwichWeapon = new BoolReactiveProperty();

    public WeaponDatas TargetWeapon { get => _targetWeapon; set => _targetWeapon = value; }
    public IReadOnlyReactiveProperty<bool> IsSwichWeapon => _isSwichWeapon;



    /// <summary>
    /// ���C������E�T�u����̑������{�^���Ő؂�ւ���֐�
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
    /// ����̑���
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





