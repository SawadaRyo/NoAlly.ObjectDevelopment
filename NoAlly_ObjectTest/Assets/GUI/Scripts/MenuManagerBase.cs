using System;
using UnityEngine;

public class MenuManagerBase : UIObjectBase
{
    [SerializeField, Header("MenuPanel�̏����I�����")]
    SelectObjecArrayBase _firstSelectObjects = null;

    [Tooltip("�I�𒆂̃{�^��")]
    SelectObject _targetButton = default;
    [Tooltip("���ݓW�J���̃��j���[���")]
    SelectObjecArrayBase _currentMenuPanel = null;
    [Tooltip("�ЂƂO�̃��j���[���")]
    SelectObjecArrayBase _beforeMenuPanel = null;

    

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
            Array.ForEach(_firstSelectObjects.Childlen, childlen =>
            {
                Array.ForEach(childlen.ChildArrays, x => x.Extended());
            });
            _targetButton = _firstSelectObjects.Select();
        }
        else
        {
            _targetButton.IsSelect(false);
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
        _targetButton.IsSelect(false);
        _targetButton = _currentMenuPanel.Select(x, y);
    }

    /// <summary>
    /// ���莞�Ԑ�
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjecArrayBase selectObjecArray)
        {
            _currentMenuPanel.IsSelect(false); //���O�܂œW�J���Ă�����ʂ����
            _beforeMenuPanel = selectObjecArray.Perent; //�ЂƂO�̉�ʂ��w��
            _currentMenuPanel = selectObjecArray; //���݂̉�ʂ��w��
            Array.ForEach(_currentMenuPanel.Childlen, childlen =>
            {
                Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(true));
            });
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
        _currentMenuPanel.IsSelect(false);
        Array.ForEach(_currentMenuPanel.Childlen, childlen =>
        {
            Array.ForEach(childlen.ChildArrays, x => x.ActiveUIObject(false));
        });
        _currentMenuPanel = _beforeMenuPanel;
        if (_currentMenuPanel.Perent
         && _currentMenuPanel.Perent is SelectObjecArrayBase selectObjecPerent)
        {
            _beforeMenuPanel = selectObjecPerent;
        }
        _targetButton = _currentMenuPanel.Select();
    }
}
