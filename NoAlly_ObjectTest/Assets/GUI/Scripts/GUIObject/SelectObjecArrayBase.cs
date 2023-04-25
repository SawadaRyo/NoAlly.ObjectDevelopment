using UnityEngine;
using DimensionalArray;
using System;

public class SelectObjecArrayBase : SelectObject
{
    [SerializeField, Header("このオブジェクトのメニュー深度")]
    int _depthOfMenu = 0;
    [SerializeField, Header("このオブジェクトの子関係にあるボタンオブジェクト")]
    protected GenericArray<SelectObject>[] _childlenArray = null;

    [Tooltip("")]
    (int, int) _currentCross = (0, 0);

    public int DepthOfMenu => _depthOfMenu;
    public GenericArray<SelectObject>[] Childlen => _childlenArray;
    protected virtual void SetButtonEvent() { }

    public override void Initialize(SelectObjecArrayBase perent)
    {
        SetButtonEvent();
        base.Initialize(perent);
        for (int y = 0; y < _childlenArray.Length; y++)
        {
            for (int x = 0; x < _childlenArray[y].ChildArrays.Length; x++)
            {
                _childlenArray[y].ChildArrays[x].Initialize(this);
            }
        }
    }
    public override void Closed()
    {
        base.Closed();
        Array.ForEach(_childlenArray, childlen =>
        {
            Array.ForEach(childlen.ChildArrays, x => x.Closed());
        });
    }
    public SelectObject Select()
    {
        _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1].IsSelect(true);
        return _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1];
    }
    public SelectObject Select(int x, int y)
    {
        if (x < 0)
        {
            _currentCross.Item1--;
            if (_currentCross.Item1 < 0)
            {
                _currentCross.Item1 = _childlenArray[y].ChildArrays.Length - 1;
            }
        }
        else if (x > 0)
        {
            _currentCross.Item1++;
            if (_currentCross.Item1 >= _childlenArray[y].ChildArrays.Length)
            {
                _currentCross.Item1 = 0;
            }

        }
        else if (y < 0)
        {
            _currentCross.Item2++;
            if (_currentCross.Item2 >= _childlenArray.Length)
            {
                _currentCross.Item2 = 0;
            }
        }
        else if (y > 0)
        {
            _currentCross.Item2--;
            if (_currentCross.Item2 < 0)
            {
                _currentCross.Item2 = _childlenArray.Length - 1;
            }
        }
        _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1].IsSelect(true);
        return _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1];
    }
}
