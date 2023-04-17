using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class MenuHanderBase : MonoBehaviour, IMenuHander
{
    [SerializeField, Tooltip("ボタン選択のインターバル")]
    float _interval = 0.3f;

    [Tooltip("メニュー画面の開閉確認")]
    BoolReactiveProperty _isOpen = new();
    ReactiveProperty<(int, int)> _inputCross = new();
    [Tooltip("決定入力(ReactiveProperty)")]
    BoolReactiveProperty _reactiveIsDiside = new();
    [Tooltip("決定入力(ReactiveProperty)")]
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
            // メニューのボタン操作
            _reactiveIsDiside.Value = Input.GetButtonDown("Decision"); //決定
            _reactiveIsCansel.Value = Input.GetButtonDown("CanselButton"); //戻る
            int h = (int)Input.GetAxisRaw("CrossKeyH"); //横入力
            int v = (int)Input.GetAxisRaw("CrossKeyV"); //縦入力
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
