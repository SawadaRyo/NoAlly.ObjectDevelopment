using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectObject
{
    /// <summary>
    /// ���̃I�u�W�F�N�g�̐e�֌W
    /// </summary>
    public SelectObjecArrayBase Perent { get; }
    /// <summary>
    /// �Q�[�����s��������
    /// </summary>
    public void Initialize(SelectObjecArrayBase perent);

    /// <summary>
    /// �I�������s�֐�
    /// </summary>
    /// <param name="isSelect"></param>
    public void IsSelect(bool isSelect);
    /// <summary>
    /// �{�^���̃C�x���g�����s����
    /// </summary>
    public void DoEvent();
    /// <summary>
    /// ���j���[�W�J�����s�֐�
    /// </summary>
    public void Extended();
    /// <summary>
    /// ���j���[���[���s�֐�
    /// </summary>
    public void Closed();
}
