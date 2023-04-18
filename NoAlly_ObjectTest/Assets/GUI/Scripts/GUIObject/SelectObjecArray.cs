using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DimensionalArray;
using System;

public class SelectObjecArray : SelectObject
{
    [SerializeField, Header("このオブジェクトのメニュー深度")]
    int _depthOfMenu = 0;
    [SerializeField, Header("このオブジェクトの子関係にあるボタンオブジェクト")]
    protected GenericArray<SelectObject>[] _childlenArray = null;

    [Tooltip("")]
    (int, int) _currentCross = (0, 0);

    public int DepthOfMenu => _depthOfMenu;
    public SelectObject Childlen(int x, int y) => _childlenArray[y].ChildArrays[x];

    public override void Initialize(SelectObjecArray perent)
    {
        base.Initialize(perent);
        for (int y = 0; y < _childlenArray.Length; y++)
        {
            for (int x = 0; x < _childlenArray[y].ChildArrays.Length; x++)
            {
                _childlenArray[y].ChildArrays[x].Initialize(this);
            }
        }
    }
    public SelectObject Select()
    {
        _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1].Selected(true);
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
        _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1].Selected(true);
        return _childlenArray[_currentCross.Item2].ChildArrays[_currentCross.Item1];
    }

    public override void Extended()
    {
        base.Extended();
        Array.ForEach(_childlenArray, childlen =>
        {
            Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(true));
        });
    }
    public override void Closed()
    {
        Selected(false);
        base.Closed();
        Array.ForEach(_childlenArray, childlen =>
        {
            if (_depthOfMenu != 0)
            {
                Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(false));
            }
            else
            {
                Array.ForEach(childlen.ChildArrays, x => x.Closed());
            }
        });
    }
}
