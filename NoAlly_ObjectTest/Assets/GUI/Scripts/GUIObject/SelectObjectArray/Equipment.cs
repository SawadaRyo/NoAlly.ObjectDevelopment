using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �q�I�u�W�F�N�g�̃{�^���̏������Ǘ�����N���X
/// </summary>

public class Equipment : SelectObjecArrayBase
{
    [SerializeField]
    CommandType _commandType = CommandType.NONE;
    [SerializeField]
    Image _weaponImage;


    [Tooltip("�������̕���Ƒ���")]
    (WeaponType, WeaponType, ElementType) _isEquipment = new();
    ReactiveProperty<WeaponType> _mainWeapon = new();
    ReactiveProperty<WeaponType> _subWeapon = new();
    ReactiveProperty<ElementType> _elementType = new();

    public IReadOnlyReactiveProperty<WeaponType> MainWeapon => _mainWeapon;
    public IReadOnlyReactiveProperty<WeaponType> SubWeapon => _subWeapon;
    public IReadOnlyReactiveProperty<ElementType> Element => _elementType;

    

    /// <summary>
    /// �q�I�u�W�F�N�g�̃{�^���C�x���g�����ꂼ��ݒ肷��
    /// </summary>
    protected override void SetButtonEvent()
    {
        for (int y = 0; y < _childlenArray.Length; y++)
        {
            for (int x = 0; x < _childlenArray[y].ChildArrays.Length; x++)
            {
                int index = x;
                WeaponSelect weaponSelect = _childlenArray[y].ChildArrays[x] as WeaponSelect;
                switch (_commandType)
                {
                    case CommandType.MAINWEAPON:
                        weaponSelect.Event.onClick.AddListener(() => EquipmentWeapon(weaponSelect,CommandType.MAINWEAPON, (WeaponType)index));
                        break;
                    case CommandType.SUBWEAPON:
                        weaponSelect.Event.onClick.AddListener(() => EquipmentWeapon(weaponSelect,CommandType.SUBWEAPON, (WeaponType)index));
                        break;
                    case CommandType.ELEMENT:
                        weaponSelect.Event.onClick.AddListener(() => EquipmentElement(weaponSelect,(ElementType)index));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    

    /// <summary> ���������؂�ւ���</summary>
    /// <param name="weaponName"></param>
    /// <param name="type"></param>
    public void EquipmentWeapon(WeaponSelect weaponSelect, CommandType type, WeaponType weaponName)
    {
        WeaponType beforeWeapons = default;
        if (type == CommandType.MAINWEAPON)
        {
            beforeWeapons = _isEquipment.Item1;
            _isEquipment.Item1 = weaponName;
            if (_subWeapon.Value == _mainWeapon.Value) //Main��Sub�̑������킪�����������ꍇ���ꂼ��̑�����������ւ���
            {
                _isEquipment.Item2 = beforeWeapons;
            }
        }
        else if (type == CommandType.SUBWEAPON)
        {
            beforeWeapons = _isEquipment.Item2;
            _isEquipment.Item2 = weaponName;
            if (_isEquipment.Item1 == _isEquipment.Item2) //Sub��Main�̑������킪�����������ꍇ���ꂼ��̑�����������ւ���
            {
                _isEquipment.Item1 = beforeWeapons;
            }
        }
        _weaponImage.sprite = weaponSelect.SwitchImage;
    }
    /// <summary>
    /// ������؂�ւ���
    /// </summary>
    /// <param name="element"></param>
    public void EquipmentElement(WeaponSelect weaponSelect, ElementType element)
    {
        _isEquipment.Item3 = element;
        _weaponImage.sprite = weaponSelect.SwitchImage;
    }
    public override void MenuClosed()
    {
        base.MenuClosed();
        switch (_commandType)
        {
            case CommandType.MAINWEAPON:
                _mainWeapon.Value = _isEquipment.Item1;
                break;
            case CommandType.SUBWEAPON:
                _subWeapon.Value = _isEquipment.Item2;
                break;
            case CommandType.ELEMENT:
                _elementType.Value = _isEquipment.Item3;
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        _mainWeapon.Dispose();
        _subWeapon.Dispose();
        _elementType.Dispose();
    }
}
