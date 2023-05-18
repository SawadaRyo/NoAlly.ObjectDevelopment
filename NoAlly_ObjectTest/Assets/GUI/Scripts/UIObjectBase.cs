using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectBase : MonoBehaviour, ISelectObject
{
    [SerializeField, Header("�I�u�W�F�N�g��Image")]
    protected Image[] _objectImage = null;
    [SerializeField, Header("�I�u�W�F�N�g��Animator")]
    protected Animator _objectAnimator = null;
    [SerializeField, Header("���莞�̃C�x���g")]
    protected Button _event;

    [Tooltip("")]
    protected bool _isActive = false;
    [Tooltip("�{�^���̐e�֌W")]
    protected SelectObjecArrayBase _perent = null;
    [Tooltip("�{�^���̏��")]
    protected ButtonState _state = ButtonState.NONE;

    public Animator ObjectAnimator => _objectAnimator;
    public bool IsActive => _isActive;
    public SelectObjecArrayBase Perent => _perent;
    public Button Event => _event;

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
                        //�I����
                    }
                    _state = ButtonState.NONE;
                    break;
                case ButtonState.DISIDED:
                    if (_event.image)
                    {
                        //���莞
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
