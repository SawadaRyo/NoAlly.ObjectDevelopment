using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : UIObjectBase
{
    [SerializeField, Header("MenuPanel�̏����I�����")]
    SelectObjecArray _firstSelectObjects = null;

    [Tooltip("�I�𒆂̃{�^��")]
    SelectObject _targetButton = default;
    [Tooltip("���ݓW�J���̃��j���[���")]
    SelectObjecArray _currentMenuPanel = null;
    [Tooltip("�ЂƂO�̃��j���[���")]
    SelectObjecArray _beforeMenuPanel = null;



    /// <summary>
    /// �������֐�
    /// </summary>
    public void Initialize()
    {
        ActiveUIObject(false);
        _firstSelectObjects.Initialize(null);
        _targetButton = _firstSelectObjects.Select(0, 0);
        _currentMenuPanel = _firstSelectObjects;
    }

    /// <summary>
    /// ���j���[�W�J�֐�
    /// </summary>
    /// <param name="isMenuOpen">�W�J����</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _firstSelectObjects.Extended();
            _targetButton = _firstSelectObjects.Select();
            _targetButton.Selected(true);
        }
        else
        {
            _targetButton.Selected(false);
            _firstSelectObjects.Closed();
        }
        ActiveUIObject(isMenuOpen);
    }

    /// <summary>
    /// �{�^���I��
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SelectTaretButton(int x, int y)
    {
        _targetButton.Selected(false);
        _targetButton = _currentMenuPanel.Select(x, y);
    }

    /// <summary>
    /// ���莞�Ԑ�
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjecArray selectObjecArray)
        {
            _currentMenuPanel.Selected(false); //���O�܂œW�J���Ă�����ʂ����
            _currentMenuPanel.ActiveUIObject(false);
            _beforeMenuPanel = selectObjecArray.Perent; //�ЂƂO�̉�ʂ��w��
            _currentMenuPanel = selectObjecArray; //���݂̉�ʂ��w��
            _currentMenuPanel.Extended(); 
            _targetButton = _currentMenuPanel.Select();//���݂̉�ʂ�W�J
        }
        else if (_targetButton is SelectObject)
        {
            _targetButton.DoEvent();
        }
    }

    /// <summary>
    /// �L�����Z���֐�
    /// </summary>
    public void OnCansel()
    {
        _currentMenuPanel.Closed();
        _currentMenuPanel = _beforeMenuPanel;

        if (_currentMenuPanel.Perent
         && _currentMenuPanel.Perent is SelectObjecArray selectObjecPerent)
        {
            _beforeMenuPanel = selectObjecPerent;
        }
        _currentMenuPanel.Extended();
        _targetButton = _currentMenuPanel.Select();
    }
}
