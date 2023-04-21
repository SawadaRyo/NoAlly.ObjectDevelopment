using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : UIObjectBase, ISelectObject
{
    [SerializeField, Header("決定時のイベント")]
    Button _event;
    [SerializeField, Header("ボタンの配色")]
    Color[] _buttonColors = { Color.white, Color.yellow };
    [SerializeField, Tooltip("ボタンの種類")]
    ButtonKind _buttonKind;

    [Tooltip("ボタンの親関係")]
    SelectObjecArrayBase _perent = null;
    [Tooltip("ボタンの状態")]
    ButtonState _state = ButtonState.NONE;

    public SelectObjecArrayBase Perent => _perent;
    public Button Event => _event;
    public ButtonKind ButtonKind => _buttonKind;

    public virtual void Initialize(SelectObjecArrayBase perent)
    {
        _perent = perent;
        ActiveUIObject(false);
    }
    public virtual void IsSelect(bool isSelect)
    {
        if (!_event) return;
        if (isSelect)
        {
            if (_state != ButtonState.DISIDED)
            {
                _state = ButtonState.SELECTED;
            }
        }
        else
        {
            switch (_state)
            {
                case ButtonState.SELECTED:
                    if (_event.image)
                    {
                        _event.image.color = _buttonColors[0];
                    }
                    _state = ButtonState.NONE;
                    break;
                case ButtonState.DISIDED:
                    if (_event.image)
                    {
                        _event.image.color = _buttonColors[1];
                    }
                    break;
                default:
                    break;
            }
        }
        _objectAnimator.SetBool("IsSelect", isSelect);
    }
    public void DoEvent()
    {
        _event.onClick.Invoke();
    }
    public virtual void Extended()
    {
        ActiveUIObject(true);
    }
    public virtual void Closed()
    {
        ActiveUIObject(false);
    }
}

public enum ButtonState : int
{
    NONE, //通常
    SELECTED, //選択中
    DISIDED //決定済み
}

public enum CommandType : int
{
    MAINWEAPON, //メイン武器
    SUBWEAPON, //サブ武器
    ELEMENT, //属性
    SKILL //スキル
}

public enum ButtonKind
{
    EQUIPMENT,

}
