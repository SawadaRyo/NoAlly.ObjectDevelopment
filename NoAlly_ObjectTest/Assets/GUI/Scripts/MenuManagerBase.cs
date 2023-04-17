using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : MonoBehaviour
{
    [SerializeField, Header("MenuPanel�̔z��B(�A�^�b�`����Ƃ��͊K�w����)")]
    SelectObjecArray[] _selectObjects = null;

    [Tooltip("���݂̃��j���[�[�x")]
    int _currentDepthNum = 0;
    [Tooltip("�I�𒆂̃{�^��")]
    SelectObject _targetButton = default;
    [Tooltip("���ݓW�J���̃��j���[���")]
    SelectObjecArray _currentMenuPanel = null;



    /// <summary>
    /// �������֐�
    /// </summary>
    public void Initialize()
    {
        Array.ForEach(_selectObjects, x => x.Initialize());
        _targetButton = _selectObjects[0].Select(0, 0);
        _currentMenuPanel = _selectObjects[0];
    }

    /// <summary>
    /// ���j���[�W�J�֐�
    /// </summary>
    /// <param name="isMenuOpen">�W�J����</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _selectObjects[0].Extended();
            _targetButton = _selectObjects[0].Select(0, 0);
            _targetButton.Selected(true);
        }
        else
        {
            _targetButton.Selected(false);
            Array.ForEach(_selectObjects, x => x.Closed());
        }
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
        _targetButton.Selected(true);
    }

    /// <summary>
    /// ���莞�Ԑ�
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjecArray)
        {
            _currentMenuPanel.Disaide(false);
            if (_currentDepthNum++! >= _selectObjects.Length)
            {
                _currentDepthNum++;
                _currentMenuPanel = _selectObjects[_currentDepthNum];
            }
            _currentMenuPanel.Disaide(true);
        }
        else if (_targetButton is SelectObject)
        {
            _targetButton.Disaide();
        }
    }

    /// <summary>
    /// �L�����Z���֐�
    /// </summary>
    public void OnCansel()
    {
        _currentMenuPanel.Disaide(false);
        if (_currentDepthNum--! <= -1)
        {
            _currentDepthNum--;
            _currentMenuPanel = _selectObjects[_currentDepthNum];
        }
        _currentMenuPanel.Disaide(true);
    }
}
