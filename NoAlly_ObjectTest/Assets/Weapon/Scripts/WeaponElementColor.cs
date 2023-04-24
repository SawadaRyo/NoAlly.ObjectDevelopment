using DimensionalArray;
//using DG.T
using UnityEngine;

public class WeaponElementColor : MonoBehaviour
{
    [SerializeField, Header("�ϐF�ɂ����鎞��")]
    float _interval = 0.1f;
    [SerializeField, Header("����̑����F")]
    Color[] _weaponElementColor = new[] { Color.red, Color.yellow, Color.blue };
    [SerializeField]
    GenericArray<Renderer>[] _weaponRenderer = null;

    public void ChangeColor(WeaponType weaponType,ElementType elementType)
    {
        foreach (var weaponRenderer in _weaponRenderer[(int)weaponType].ChildArrays)
        {

        }
    }
}
