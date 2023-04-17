using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class MenuHanderBase : MonoBehaviour, IMenuHander
{
    [SerializeField, Tooltip("�{�^���I���̃C���^�[�o��")]
    float _interval = 0.3f;

    [Tooltip("���j���[��ʂ̊J�m�F")]
    BoolReactiveProperty _isOpen = new();
    ReactiveProperty<(int, int)> _inputCross = new();
    [Tooltip("�������(ReactiveProperty)")]
    BoolReactiveProperty _reactiveIsDiside = new();
    [Tooltip("�������(ReactiveProperty)")]
    BoolReactiveProperty _reactiveIsCansel = new();
    [Tooltip("")]
    Interval _canMove = default;

    public IReadOnlyReactiveProperty<bool> IsOpen => _isOpen;
    public IReadOnlyReactiveProperty<(int, int)> InputCross => _inputCross;
    public IReadOnlyReactiveProperty<bool> IsDiside => _reactiveIsDiside;
    public IReadOnlyReactiveProperty<bool> IsCansel => _reactiveIsCansel;

    public void Initialize()
    {
        _canMove = new Interval(_interval);
    }

    public void OnUpdate()
    {
        if (Input.GetButtonDown("MenuSwitch"))
        {
            _isOpen.Value = !_isOpen.Value;
        }

        if (_isOpen.Value)
        {
            // ���j���[�̃{�^������
            _reactiveIsDiside.Value = Input.GetButtonDown("Decision"); //����
            _reactiveIsCansel.Value = Input.GetButtonDown("CanselButton"); //�߂�
            int h = (int)Input.GetAxisRaw("CrossKeyH"); //������
            int v = (int)Input.GetAxisRaw("CrossKeyV"); //�c����
            if (_canMove.IsCountUp() && (h != 0 || v != 0))
            {
                _canMove.ResetTimer();
                _inputCross.Value = (h, v);
            }
        }
    }

    void OnDisable()
    {
        _inputCross.Dispose();
        _reactiveIsDiside.Dispose();
    }
}
