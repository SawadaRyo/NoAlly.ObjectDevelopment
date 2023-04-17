using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataOfWeapon
{
    public class SetWeaponData
    {
        [Tooltip("武器のスクリプタブルオブジェクト")]
        WeaponScriptableObjects _weaponScriptableObjects = null;
        WeaponDatas[] _weaponDatas = null;

        public WeaponDatas[] SetAllWeapon { get => _weaponDatas; set => _weaponDatas = value; }
        public WeaponDatas[] GetAllWeapons => _weaponDatas;
        public WeaponDatas GetWeapon(WeaponType type) => _weaponDatas[(int)type];


        public SetWeaponData(WeaponScriptableObjects weapon)
        {
            if (_weaponScriptableObjects != null) return;
            //_weaponScriptableObjects = weapon;
            //IWeaponBase[] _weaponBases = new IWeaponBase[]
            //{
            //    new WeaponSword(_weaponScriptableObjects.WeaponDatas[(int)WeaponType.SWORD]),
            //    new WeaponLance(_weaponScriptableObjects.WeaponDatas[(int)WeaponType.LANCE]),
            //    new WeaponSnip(_weaponScriptableObjects.WeaponDatas[(int)WeaponType.BOW]),
            //    new WeaponShield(_weaponScriptableObjects.WeaponDatas[(int)WeaponType.SHIELD])
            //};
            //IWeaponAction[] _weaponAction = new IWeaponAction[]
            //{
            //    new CombatAction(_weaponBases[(int)WeaponType.SWORD]),
            //    new CombatAction(_weaponBases[(int)WeaponType.LANCE]),
            //    new CombatAction(_weaponBases[(int)WeaponType.BOW]),
            //    new ArrowAction(_weaponBases[(int)WeaponType.SHIELD])
            //};

            for (int i = 0; i < _weaponDatas.Length; i++)
            {
                _weaponDatas[i] = new WeaponDatas((WeaponType)i);
            }
        }
    }
}



public class WeaponDatas
{
    [Tooltip("武器を使用しているか")]
    bool _weaponEnabled;
    [Tooltip("武器の種類")]
    WeaponType _type = WeaponType.NONE;

    public bool WeaponEnabled { get => _weaponEnabled; set => _weaponEnabled = value; }
    public WeaponType Type => _type;

    public WeaponDatas(WeaponType type)
    {
        _type = type;
    }
}

