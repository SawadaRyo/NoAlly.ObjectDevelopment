using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DimensionalArray;

public class SelectObjects : GUISelectObject
{
    [SerializeField,Header("このオブジェクトのメニュー深度")]
    int _depthOfMenu = 0;
    [SerializeField, Header("このオブジェクトの子関係にあるボタンオブジェクト")]
    protected GenericArray<GUISelectObject>[] _childlen = null;

    [Tooltip("")]
    (int, int) _currentCross = (0, 0);

    public int DepthOfMenu => _depthOfMenu; 

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override void Initializer()
    {
        base.Initializer();
        for (int y = 0; y < _childlen.Length; y++)
        {
            for (int x = 0; x < _childlen[y].ChildArrays.Length; x++)
            {
                _childlen[y].ChildArrays[x].Initializer();
            }
        }
    }

    /// <summary>
    /// コマンドボタンを選択する関数
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <returns>指定された座標のコマンドボタン</returns>
    public GUISelectObject Select(int x, int y)
    {
        _currentCross.Item1 += x;
        _currentCross.Item2 += y;
        if (_currentCross.Item1 >= _childlen[y].ChildArrays.Length)
        {
            _currentCross.Item1 = 0;
        }
        else if (_currentCross.Item1 < 0)
        {
            _currentCross.Item1 = _childlen[y].ChildArrays.Length - 1;
        }
        else if (_currentCross.Item2 >= _childlen.Length)
        {
            _currentCross.Item2 = 0;
        }
        else if (_currentCross.Item2 < 0)
        {
            _currentCross.Item1 = _childlen.Length - 1;
        }
        return _childlen[_currentCross.Item2].ChildArrays[_currentCross.Item1];
    }

    /// <summary>
    /// 決定時関数
    /// </summary>
    /// <param name="isDisaide">決定判定</param>
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
}
