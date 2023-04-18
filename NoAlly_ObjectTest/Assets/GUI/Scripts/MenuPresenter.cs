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
                _menuManager.IsMenuOpen(isMenuOpen);
            });
        _menuHander.InputCross.Skip(1)
            .Subscribe(inputCross =>
            {
                _menuManager.SelectTaretButton(inputCross.Item1, inputCross.Item2);
            });
        _menuHander.IsDiside.Skip(1)
            .Subscribe(isDiside =>
            {
                _menuManager.OnDisaide();
            });
        _menuHander.IsCansel.Skip(1)
            .Subscribe(isCansel =>
            {
                _menuManager.OnCansel();
            });
    }
}
