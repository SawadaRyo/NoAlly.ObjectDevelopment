using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectObject
{
    /// <summary>
    /// ���̃I�u�W�F�N�g�̐e�֌W
    /// </summary>
    public SelectObjecArray Perent { get; }
    /// <summary>
    /// �Q�[�����s��������
    /// </summary>
    public void Initialize(SelectObjecArray perent);

    /// <summary>
    /// �I�������s�֐�
    /// </summary>
    /// <param name="isSelect"></param>
    public void Selected(bool isSelect);
}
