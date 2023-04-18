using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : UIObjectBase,ISelectObject
{
    [SerializeField, Header("���莞�̃C�x���g")]
    protected Button _event;
    [SerializeField, Header("�{�^���̔z�F")]
    protected Color[] _buttonColors = { Color.white, Color.yellow };

    [Tooltip("")] SelectObjecArray _perent = null;
    [Tooltip("�{�^���̏��")]
    protected ButtonState _state = ButtonState.NONE;

    public SelectObjecArray Perent => _perent;

    public virtual void Initialize(SelectObjecArray perent)
    {
        _perent = perent;
        ActiveUIObject(false);
    }
    /// <summary>
    /// �I�������s�֐�
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
    NONE, //�ʏ�
    SELECTED, //�I��
    DISIDED //����ς�
}

public enum CommandType : int
{
    MAIN, //���C������
    SUB, //�T�u����
    ELEMENT //����
}
