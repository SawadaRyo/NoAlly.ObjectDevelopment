using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ����E�G���`�����g��ύX���邽�߂̃R���|�[�l���g
/// </summary>

public class WeaponMenu : MonoBehaviour
{
    [Tooltip("�������̕���Ƒ���")]
    ReactiveProperty<WeaponType> _mainWeapon = new ReactiveProperty<WeaponType>();
    ReactiveProperty<WeaponType> _subWeapon = new ReactiveProperty<WeaponType>();
    ReactiveProperty<ElementType> _elementType = new ReactiveProperty<ElementType>();

    public IReadOnlyReactiveProperty<ElementType> Element => _elementType;
    public IReadOnlyReactiveProperty<WeaponType> MainWeapon => _mainWeapon;
    public IReadOnlyReactiveProperty<WeaponType> SubWeapon => _subWeapon;

   

    /// <summary> ���������؂�ւ���</summary>
    /// <param name="weaponName"></param>
    /// <param name="commandType"></param>
    public void EquipmentWeapon(int weapon)
    {
        WeaponType weaponType = (WeaponType)weapon;
        _mainWeapon.Value = weaponType;
    }
    /// <summary>
    /// ������؂�ւ���
    /// </summary>
    /// <param name="element"></param>
    public void EquipmentElement(int element)
    {
        ElementType e = (ElementType)element;
        _elementType.Value = e;
    }



    private void OnDisable()
    {
        _mainWeapon.Dispose();
        _subWeapon.Dispose();
        _elementType.Dispose();
    }
}



