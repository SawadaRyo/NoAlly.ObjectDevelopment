using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    MenuHanderBase _menuHander;

    [SerializeField, Tooltip("")]
    MenuManagerBase _menuManager;


    void Start()
    {
        _menuHander.Initialize();
        _menuManager.Initialize();
        ObjectSelect();
    }

    void Update()
    {
        _menuHander.OnUpdate();
    }

    void ObjectSelect()
    {
        _menuHander.IsOpen
            .Subscribe(isMenuOpen =>
            {
                _menuManager.IsMenuOpen(isMenuOpen);
            });
        if (_menuHander.IsOpen.Value)
        {

            _menuHander.InputCross
                .Subscribe(inputCross =>
                {
                    _menuManager.SelectTaretButton(inputCross.Item1, inputCross.Item2);
                });
            _menuHander.IsDiside
                .Subscribe(isDiside =>
                {
                    _menuManager.OnDisaide();
                });
            _menuHander.IsCansel
                .Subscribe(isCansel =>
                {
                    _menuManager.OnCansel();
                });
        }
    }
}
