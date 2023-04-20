using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DataOfWeapon;

public class WeaponPresenter : MonoBehaviour
{
    //[SerializeField, Header("WeaponScriptableObjects�{��")]
    //WeaponScriptableObjects _weaponScriptableObjects;

    [Space(15)]
    [Header("Model")]
    [SerializeField, Header("WeaponEquipment���i�[����֐�")]
    WeaponMenu _weaponEquipment = null;

    [Space(15)]
    [Header("View")]
    [SerializeField, Header("WeaponProcessing���i�[����֐�")]
    WeaponProcessing _weaponProcessing = null;

    SetWeaponData _weaponData = null;
    void Awake()
    {
        //_weaponData = new SetWeaponData(_weaponScriptableObjects);
        WeaponEquipmentState();
        WeaponProcessingState();
    }
    void WeaponEquipmentState()
    {
        //����̑������
        _weaponEquipment.MainWeapon
            .Subscribe(mainWeapon =>
            {
                _weaponProcessing.SetEquipment(mainWeapon, CommandType.MAINWEAPON);
                //_weaponProcessing.TargetWeapon.Base.WeaponModeToElement(_weaponEquipment.Element.Value);
            }).AddTo(this);
        _weaponEquipment.Element
            .Subscribe(element =>
            {
                _weaponProcessing.SetElement(element);
            }).AddTo(this);
    }
    void WeaponProcessingState()
    {
        _weaponProcessing.IsSwichWeapon
           .Subscribe(isSwich =>
           {
               

           }).AddTo(this);
    }
}
