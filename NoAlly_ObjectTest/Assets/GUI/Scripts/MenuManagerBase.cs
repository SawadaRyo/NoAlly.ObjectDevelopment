using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : MonoBehaviour
{
    [SerializeField, Header("MenuPanelの配列。(アタッチするときは階層順に)")]
    SelectObjects[] _selectObjects = null;

    [Tooltip("現在のメニュー深度")]
    int _currentDepthNum = 0;
    [Tooltip("選択中のボタン")]
    GUISelectObject _targetButton = default;
    [Tooltip("現在展開中のメニュー画面")]
    SelectObjects _currentMenuPanel = null;

    /// <summary>
    /// 選択中のボタン
    /// </summary>
    public GUISelectObject TargetButton { get => _targetButton; set => _targetButton = value; }
    /// <summary>
    /// 現在展開中のメニュー画面
    /// </summary>
    public SelectObjects CurrentMenuPanel { get => _currentMenuPanel; set => _currentMenuPanel = value; }

    /// <summary>
    /// 初期化関数
    /// </summary>
    public void Initialize()
    {
        Array.ForEach(_selectObjects, x => x.Initializer());
        //_targetButton.Selected(true);
    }

    /// <summary>
    /// メニュー展開関数
    /// </summary>
    /// <param name="isMenuOpen">展開判定</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _selectObjects[0].ActiveUIObject(isMenuOpen);
        }
        else
        {
            Array.ForEach(_selectObjects, x => x.ActiveUIObject(isMenuOpen));
        }
    }

    /// <summary>
    /// 決定時間数
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjects)
        {
            _currentMenuPanel.Disaide(false);
            if (_currentDepthNum++! >= _selectObjects.Length)
            {
                _currentDepthNum++;
                _currentMenuPanel = _selectObjects[_currentDepthNum];
            }
            _currentMenuPanel.Disaide(true);
        }
        else if(_targetButton is GUISelectObject)
        {
            _targetButton.Disaide();
        }
    }

    /// <summary>
    /// キャンセル関数
    /// </summary>
    public void OnCansel()
    {
        _currentMenuPanel.Disaide(false);
        if (_currentDepthNum--! <= -1)
        {
            _currentDepthNum--;
            _currentMenuPanel = _selectObjects[_currentDepthNum];
        }
        _currentMenuPanel.Disaide(true);
    }
}
