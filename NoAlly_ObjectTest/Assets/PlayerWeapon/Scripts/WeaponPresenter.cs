using UnityEngine;
using UniRx;
using DataOfWeapon;

public class WeaponPresenter : MonoBehaviour
{
    //[SerializeField, Header("WeaponScriptableObjectsñ{ëÃ")]
    //WeaponScriptableObjects _weaponScriptableObjects;

    [Space(15)]
    [Header("Model")]
    [SerializeField, Header("WeaponEquipmentÇäiî[Ç∑ÇÈä÷êî")]
    Equipment[] _weaponEquipment = null;

    ReactiveCollection<Equipment> _equipment = new();

    [Space(15)]
    [Header("View")]
    [SerializeField, Header("WeaponProcessingÇäiî[Ç∑ÇÈä÷êî")]
    WeaponProcessing _weaponProcessing = null;
    [SerializeField, Header("")]
    WeaponElementColor _weaponElementColor = null;

    WeaponType _cullentWeaponType;

    public WeaponType CullentWeaponType => _cullentWeaponType;

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
        //ïêäÌÇÃëïîıèÓïÒ
        _weaponEquipment[index].MainWeapon.Skip(1)
            .Subscribe(mainWeapon =>
            {
                _weaponProcessing.SetEquipment(mainWeapon, CommandType.MAINWEAPON);
                _cullentWeaponType = mainWeapon;
                Debug.Log(mainWeapon);
            }).AddTo(this);
        _weaponEquipment[index].SubWeapon.Skip(1)
           .Subscribe(subWeapon =>
           {
               _weaponProcessing.SetEquipment(subWeapon, CommandType.SUBWEAPON);
               _cullentWeaponType = subWeapon;
               Debug.Log(subWeapon);
           }).AddTo(this);
        _weaponEquipment[index].Element.Skip(1)
            .Subscribe(element =>
            {
                _weaponProcessing.SetElement(element);
                if (element != ElementType.RIGIT)
                {
                    _weaponElementColor.IsActiveElement(_cullentWeaponType,element);
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
