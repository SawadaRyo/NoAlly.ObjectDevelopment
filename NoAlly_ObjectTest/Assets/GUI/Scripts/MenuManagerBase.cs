using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : UIObjectBase
{
    [SerializeField, Header("MenuPanelの初期選択画面")]
    SelectObjecArray _firstSelectObjects = null;

    [Tooltip("選択中のボタン")]
    SelectObject _targetButton = default;
    [Tooltip("現在展開中のメニュー画面")]
    SelectObjecArray _currentMenuPanel = null;
    [Tooltip("ひとつ前のメニュー画面")]
    SelectObjecArray _beforeMenuPanel = null;



    /// <summary>
    /// 初期化関数
    /// </summary>
    public void Initialize()
    {
        ActiveUIObject(false);
        _firstSelectObjects.Initialize(null);
        _targetButton = _firstSelectObjects.Select(0, 0);
        _currentMenuPanel = _firstSelectObjects;
    }

    /// <summary>
    /// メニュー展開関数
    /// </summary>
    /// <param name="isMenuOpen">展開判定</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _firstSelectObjects.Extended();
            _targetButton = _firstSelectObjects.Select();
            _targetButton.Selected(true);
        }
        else
        {
            _targetButton.Selected(false);
            _firstSelectObjects.Closed();
        }
        ActiveUIObject(isMenuOpen);
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
    }

    /// <summary>
    /// 決定時間数
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjecArray selectObjecArray)
        {
            _currentMenuPanel.Selected(false); //直前まで展開していた画面を閉じる
            _currentMenuPanel.ActiveUIObject(false);
            _beforeMenuPanel = selectObjecArray.Perent; //ひとつ前の画面を指定
            _currentMenuPanel = selectObjecArray; //現在の画面を指定
            _currentMenuPanel.Extended(); 
            _targetButton = _currentMenuPanel.Select();//現在の画面を展開
        }
        else if (_targetButton is SelectObject)
        {
            _targetButton.DoEvent();
        }
    }

    /// <summary>
    /// キャンセル関数
    /// </summary>
    public void OnCansel()
    {
        _currentMenuPanel.Closed();
        _currentMenuPanel = _beforeMenuPanel;

        if (_currentMenuPanel.Perent
         && _currentMenuPanel.Perent is SelectObjecArray selectObjecPerent)
        {
            _beforeMenuPanel = selectObjecPerent;
        }
        _currentMenuPanel.Extended();
        _targetButton = _currentMenuPanel.Select();
    }
}
