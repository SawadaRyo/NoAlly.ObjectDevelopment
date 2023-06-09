using System;
using DimensionalArray;
using DG.Tweening;
using UnityEngine;

public class WeaponElementColor : MonoBehaviour
{
    [SerializeField, Header("変色にかかる時間")]
    float _interval = 0.1f;
    [SerializeField]
    GenericArray<Renderer>[] _weaponRenderer = null;
    [SerializeField, Header("武器の属性色")]
    Color[] _weaponElementColor = new[] {new Color(0f,0f,0f,0f), Color.red, Color.yellow, Color.blue };

    [Tooltip("現在の属性")]
    ElementType _currentElement;

    public void IsActiveElement(WeaponType weaponType, ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.RIGIT:
                Array.ForEach(_weaponRenderer[(int)weaponType].ChildArrays, x => x.enabled = false);
                break;
            default:
                Array.ForEach(_weaponRenderer[(int)weaponType].ChildArrays, x => x.enabled = true);
                break;
        }
        ChangeColor(elementType);
    }

    void ChangeColor(ElementType elementType)
    {
        if (elementType == _currentElement) return;
        foreach (var weaponRenderer in _weaponRenderer[(int)elementType].ChildArrays)
        {
            DOTween.To(() => weaponRenderer.material.color,
                 n => weaponRenderer.material.color = n,
                 _weaponElementColor[(int)elementType],
                 duration: _interval).OnComplete(() => _currentElement = elementType);
        }
    }
}
