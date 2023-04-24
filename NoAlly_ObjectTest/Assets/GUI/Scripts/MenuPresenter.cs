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
        _menuHander.IsOpen.Skip(1)
            .Subscribe(isMenuOpen =>
            {
                _menuManager.IsMenuOpen();
            });
        _menuHander.InputCross.Skip(1)
            .Subscribe(inputCross =>
            {
                _menuManager.SelectTargetButton(inputCross.Item1, inputCross.Item2);
            });
        _menuHander.IsDiside.Skip(1)
            .Subscribe(isDiside =>
            {
                if (!isDiside) return;
                _menuManager.OnDisaide();
            });
        _menuHander.IsCansel.Skip(1)
            .Subscribe(isCansel =>
            {
                if(!isCansel) return;
                _menuManager.OnCansel();
            });
    }
}
