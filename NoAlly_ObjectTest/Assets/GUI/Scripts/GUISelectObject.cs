using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISelectObject : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g��Image")]
    Image[] _objectImage = null;
    [SerializeField, Header("�I�u�W�F�N�g��Animator")]
    protected Animator _objectAnimator = null;
    [SerializeField, Header("���莞�̃C�x���g")]
    protected Button _event;
    [SerializeField, Header("�{�^���̔z�F")]
    protected Color[] _buttonColors = { Color.white, Color.yellow };

    [Tooltip("�{�^���̏��")]
    protected ButtonState _state = ButtonState.NONE;

    /// <summary>
    /// �Q�[�����s��������
    /// </summary>
    public virtual void Initializer()
    {
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

    /// <summary>
    /// ���莞���s�֐�
    /// </summary>
    public virtual void Disaide()
    {
        _event.onClick.Invoke();
        //_objectAnimator.SetBool("IsDisaide", isDisaide);
    }

    /// <summary>
    /// ���莞���s�֐�
    /// </summary>
    /// <param name="isDisaide">���蔻��</param>
    public virtual void Disaide(bool isDisaide)
    {
        _event.onClick.Invoke();
        //_objectAnimator.SetBool("IsDisaide", isDisaide);
    }
    /// <summary>
    /// �I�u�W�F�N�g�̕\��
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
