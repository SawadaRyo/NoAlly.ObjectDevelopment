using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : SelectObjecArrayBase
{
    [SerializeField]
    CommandType _commandType = CommandType.NONE;


    [Tooltip("‘•”õ’†‚Ì•Ší‚Æ‘®«")]
    (WeaponType, WeaponType, ElementType) _isEquipment = new();
    ReactiveProperty<WeaponType> _mainWeapon = new();
    ReactiveProperty<WeaponType> _subWeapon = new();
    ReactiveProperty<ElementType> _elementType = new();

    public IReadOnlyReactiveProperty<WeaponType> MainWeapon => _mainWeapon;
    public IReadOnlyReactiveProperty<WeaponType> SubWeapon => _subWeapon;
    public IReadOnlyReactiveProperty<ElementType> Element => _elementType;

    protected override void SetButtonEvent()
    {
        for (int y = 0; y < _childlenArray.Length; y++)
        {
            for (int x = 0; x < _childlenArray[y].ChildArrays.Length; x++)
            {
                int index = x;
                switch (_commandType)
                {
                    case CommandType.MAINWEAPON:
                        _childlenArray[y].ChildArrays[x].Event.onClick.AddListener(() => EquipmentWeapon(CommandType.MAINWEAPON, (WeaponType)index));
                        break;
                    case CommandType.SUBWEAPON:
                        _childlenArray[y].ChildArrays[x].Event.onClick.AddListener(() => EquipmentWeapon(CommandType.SUBWEAPON, (WeaponType)index));
                        break;
                    case CommandType.ELEMENT:
                        _childlenArray[y].ChildArrays[x].Event.onClick.AddListener(() => EquipmentElement((ElementType)index));
                        break;
                    default:
                        break;
                }
            }
        }

    }

    /// <summary> ‘•”õ•Ší‚ğØ‚è‘Ö‚¦‚é</summary>
    /// <param name="weaponName"></param>
    /// <param name="type"></param>
    public void EquipmentWeapon(CommandType type, WeaponType weaponName)
    {
        WeaponType beforeWeapons = default;
        if (type == CommandType.MAINWEAPON)
        {
            beforeWeapons = _isEquipment.Item1;
            _isEquipment.Item1 = weaponName;
            if (_subWeapon.Value == _mainWeapon.Value) //Main‚ÆSub‚Ì‘•”õ•Ší‚ª“¯‚¶‚¾‚Á‚½ê‡‚»‚ê‚¼‚ê‚Ì‘•”õ•Ší‚ğ“ü‚ê‘Ö‚¦‚é
            {
                _isEquipment.Item2 = beforeWeapons;
            }
        }
        else if (type == CommandType.SUBWEAPON)
        {
            beforeWeapons = _isEquipment.Item2;
            _isEquipment.Item2 = weaponName;
            if (_isEquipment.Item1 == _isEquipment.Item2) //Sub‚ÆMain‚Ì‘•”õ•Ší‚ª“¯‚¶‚¾‚Á‚½ê‡‚»‚ê‚¼‚ê‚Ì‘•”õ•Ší‚ğ“ü‚ê‘Ö‚¦‚é
            {
                _isEquipment.Item1 = beforeWeapons;
            }
        }
        //_switchImage.material = _
    }
    /// <summary>
    /// ‘®«‚ğØ‚è‘Ö‚¦‚é
    /// </summary>
    /// <param name="element"></param>
    public void EquipmentElement(ElementType element)
    {
        _isEquipment.Item3 = element;
    }

    public override void Closed()
    {
        base.Closed();
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
