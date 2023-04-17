using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectObject
{
    /// <summary>
    /// �Q�[�����s��������
    /// </summary>
    public void Initialize();

    /// <summary>
    /// �I�������s�֐�
    /// </summary>
    /// <param name="isSelect"></param>
    public void Selected(bool isSelect);

    /// <summary>
    /// ���莞���s�֐�
    /// </summary>
    public void Disaide();

    /// <summary>
    /// ���莞���s�֐�
    /// </summary>
    /// <param name="isDisaide">���蔻��</param>
    public void Disaide(bool isDisaide);

    /// <summary>
    /// �R���|�[�l���g�W�J���ɌĂԊ֐�
    /// </summary>
    public void Extended();

    /// <summary>
    /// �R���|�[�l���g�k�����ɌĂԊ֐�
    /// </summary>
    public void Closed();
}
