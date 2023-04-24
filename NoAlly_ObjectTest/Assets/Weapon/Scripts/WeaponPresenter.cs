using UnityEngine;
using UniRx;
using DataOfWeapon;

public class WeaponPresenter : MonoBehaviour
{
    //[SerializeField, Header("WeaponScriptableObjects–{‘Ì")]
    //WeaponScriptableObjects _weaponScriptableObjects;

    [Space(15)]
    [Header("Model")]
    [SerializeField, Header("WeaponEquipment‚ğŠi”[‚·‚éŠÖ”")]
    Equipment[] _weaponEquipment = null;

    ReactiveCollection<Equipment> _equipment = new();

    [Space(15)]
    [Header("View")]
    [SerializeField, Header("WeaponProcessing‚ğŠi”[‚·‚éŠÖ”")]
    WeaponProcessing _weaponProcessing = null;
    [SerializeField, Header("")]
    WeaponElementColor _weaponElementColor = null;

    void Awake()
    {
        for (int i = 0; i < _weaponEquipment.Length; i++)
        {
            int index = i;
            _equipment.Add(_weaponEquipment[index]);
            WeaponEquipmentState(index);
        }
        //_weaponData = new SetWeaponData(_weaponScriptableObjects);
        WeaponProcessingState();
    }
    void WeaponEquipmentState(int index)
    {
        //•Ší‚Ì‘•”õî•ñ
        _weaponEquipment[index].MainWeapon.Skip(1)
            .Subscribe(mainWeapon =>
            {
                _weaponProcessing.SetEquipment(mainWeapon, CommandType.MAINWEAPON);
                Debug.Log(mainWeapon);
            }).AddTo(this);
        _weaponEquipment[index].SubWeapon.Skip(1)
           .Subscribe(subWeapon =>
           {
               _weaponProcessing.SetEquipment(subWeapon, CommandType.SUBWEAPON);
               Debug.Log(subWeapon);
           }).AddTo(this);
        _weaponEquipment[index].Element.Skip(1)
            .Subscribe(element =>
            {
                _weaponProcessing.SetElement(element);
                if (element != ElementType.RIGIT)
                {
                    _weaponElementColor.ChangeColor(element);
                }
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
