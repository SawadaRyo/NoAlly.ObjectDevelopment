using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerBase : MonoBehaviour
{
    [SerializeField, Header("MenuPanel�̔z��B(�A�^�b�`����Ƃ��͊K�w����)")]
    SelectObjects[] _selectObjects = null;

    [Tooltip("���݂̃��j���[�[�x")]
    int _currentDepthNum = 0;
    [Tooltip("�I�𒆂̃{�^��")]
    GUISelectObject _targetButton = default;
    [Tooltip("���ݓW�J���̃��j���[���")]
    SelectObjects _currentMenuPanel = null;

    /// <summary>
    /// �I�𒆂̃{�^��
    /// </summary>
    public GUISelectObject TargetButton { get => _targetButton; set => _targetButton = value; }
    /// <summary>
    /// ���ݓW�J���̃��j���[���
    /// </summary>
    public SelectObjects CurrentMenuPanel { get => _currentMenuPanel; set => _currentMenuPanel = value; }

    /// <summary>
    /// �������֐�
    /// </summary>
    public void Initialize()
    {
        Array.ForEach(_selectObjects, x => x.Initializer());
        //_targetButton.Selected(true);
    }

    /// <summary>
    /// ���j���[�W�J�֐�
    /// </summary>
    /// <param name="isMenuOpen">�W�J����</param>
    public void IsMenuOpen(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _selectObjects[0].ActiveUIObject(isMenuOpen);
        }
        else
        {
            Array.ForEach(_selectObjects, x => x.ActiveUIObject(isMenuOpen));
        }
    }

    /// <summary>
    /// ���莞�Ԑ�
    /// </summary>
    public void OnDisaide()
    {
        if (_targetButton is SelectObjects)
        {
            _currentMenuPanel.Disaide(false);
            if (_currentDepthNum++! >= _selectObjects.Length)
            {
                _currentDepthNum++;
                _currentMenuPanel = _selectObjects[_currentDepthNum];
            }
            _currentMenuPanel.Disaide(true);
        }
        else if(_targetButton is GUISelectObject)
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
