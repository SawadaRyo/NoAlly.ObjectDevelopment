using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : MonoBehaviour
{
    [SerializeField, Header("MenuPanelの配列。(アタッチするときは階層順に)")]
    SelectObjecArray[] _selectObjects = null;

    [Tooltip("現在のメニュー深度")]
    int _currentDepthNum = 0;
    [Tooltip("選択中のボタン")]
    SelectObject _targetButton = default;
    [Tooltip("現在展開中のメニュー画面")]
    SelectObjecArray _currentMenuPanel = null;



    /// <summary>
    /// 初期化関数
    /// </summary>
    public void Initialize()
    {
        Array.ForEach(_selectObjects, x => x.Initialize());
        _targetButton = _selectObjects[0].Select(0, 0);
        _currentMenuPanel = _selectObjects[0];
    }

    /// <summary>
    /// メニュー展開関数
    /// </summary>
    /// <param name="isMenuOpen">展開判定</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _selectObjects[0].Extended();
            _targetButton = _selectObjects[0].Select(0, 0);
            _targetButton.Selected(true);
        }
        else
        {
            _targetButton.Selected(false);
            Array.ForEach(_selectObjects, x => x.Closed());
        }
    }

    /// <summary>
    /// ボタン選択
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SelectTaretButton(int x, int y) 
    {
        _targetButton.Selected(false);
        _targetButton = _currentMenuPanel.Select(x, y);
        _targetButton.Selected(true);
    }

    /// <summary>
    /// 決定時間数
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjecArray)
        {
            _currentMenuPanel.Disaide(false);
            if (_currentDepthNum++! >= _selectObjects.Length)
            {
                _currentDepthNum++;
                _currentMenuPanel = _selectObjects[_currentDepthNum];
            }
            _currentMenuPanel.Disaide(true);
        }
        else if (_targetButton is SelectObject)
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
