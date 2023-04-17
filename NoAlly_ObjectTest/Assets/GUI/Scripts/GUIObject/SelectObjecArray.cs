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
    protected GenericArray<SelectObject>[] _childlen = null;

    [Tooltip("")]
    (int, int) _currentCross = (0, 0);

    public int DepthOfMenu => _depthOfMenu;

    public override void Initialize()
    {
        base.Initialize();
        for (int y = 0; y < _childlen.Length; y++)
        {
            for (int x = 0; x < _childlen[y].ChildArrays.Length; x++)
            {
                _childlen[y].ChildArrays[x].Initialize();
            }
        }
    }

    public SelectObject Select(int x, int y)
    {
        if (x < 0)
        {
            _currentCross.Item1++;
            if (_currentCross.Item1 >= _childlen[y].ChildArrays.Length)
            {
                _currentCross.Item1 = 0;
            }
        }
        else if (x > 0)
        {
            _currentCross.Item1--;
            if (_currentCross.Item1 < 0)
            {
                _currentCross.Item1 = _childlen[y].ChildArrays.Length - 1;
            }
        }
        else if (y < 0)
        {
            _currentCross.Item2++;
            if (_currentCross.Item2 >= _childlen.Length)
            {
                _currentCross.Item2 = 0;
            }
        }
        else if (y > 0)
        {
            _currentCross.Item2--;
            if (_currentCross.Item2 < 0)
            {
                _currentCross.Item1 = _childlen.Length - 1;
            }
        }
        return _childlen[_currentCross.Item2].ChildArrays[_currentCross.Item1];
    }

    public override void Disaide(bool isDisaide)
    {
        for (int y = 0; y < _childlen.Length; y++)
        {
            for (int x = 0; x < _childlen[y].ChildArrays.Length; x++)
            {
                _childlen[y].ChildArrays[x].ActiveUIObject(isDisaide);
            }
        }
    }

    public override void Extended()
    {
        base.Extended();
        Array.ForEach(_childlen, childlen =>
        {
            Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(true));
        });
    }
    public override void Closed()
    {
        base.Closed();
        Array.ForEach(_childlen, childlen =>
        {
            Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(false));
        });
    }
}
