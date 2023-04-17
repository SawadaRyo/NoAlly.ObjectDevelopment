using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISelectObject : MonoBehaviour
{
    [SerializeField, Header("オブジェクトのImage")]
    Image[] _objectImage = null;
    [SerializeField, Header("オブジェクトのAnimator")]
    protected Animator _objectAnimator = null;
    [SerializeField, Header("決定時のイベント")]
    protected Button _event;
    [SerializeField, Header("ボタンの配色")]
    protected Color[] _buttonColors = { Color.white, Color.yellow };

    [Tooltip("ボタンの状態")]
    protected ButtonState _state = ButtonState.NONE;

    /// <summary>
    /// ゲーム実行時初期化
    /// </summary>
    public virtual void Initializer()
    {
        ActiveUIObject(false);
    }
    /// <summary>
    /// 選択時実行関数
    /// </summary>
    /// <param name="isSelect"></param>
    public virtual void Selected(bool isSelect)
    {
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

    /// <summary>
    /// 決定時実行関数
    /// </summary>
    public virtual void Disaide()
    {
        _event.onClick.Invoke();
        //_objectAnimator.SetBool("IsDisaide", isDisaide);
    }

    /// <summary>
    /// 決定時実行関数
    /// </summary>
    /// <param name="isDisaide">決定判定</param>
    public virtual void Disaide(bool isDisaide)
    {
        _event.onClick.Invoke();
        //_objectAnimator.SetBool("IsDisaide", isDisaide);
    }
    /// <summary>
    /// オブジェクトの表示
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveUIObject(bool isActive)
    {
        if (_objectAnimator != null)
        {
            _objectAnimator.enabled = isActive;
        }
        Array.ForEach(_objectImage, x => x.enabled = isActive);
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
    MAIN, //メイン武器
    SUB, //サブ武器
    ELEMENT //属性
}
