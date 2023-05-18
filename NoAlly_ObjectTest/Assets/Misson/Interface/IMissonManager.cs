using System.Collections.Generic;

public interface IMissonManager
{
    /// <summary>
    /// �~�b�V�������X�g
    /// </summary>
    public List<IMissonBase> MissonBases { get; }
    /// <summary>
    /// �~�b�V�����J�n���ɌĂԊ֐�
    /// </summary>
    public void MissonStart();
    /// <summary>
    /// �~�b�V�����N���A���ɌĂԊ֐�
    /// </summary>
    public void MissonClear();
    /// <summary>
    /// �~�b�V�������󒍂���֐�
    /// </summary>
    /// <param name="misson">�ǉ�����~�b�V���� </param>
    public void AddMisson(IMissonBase misson);
    /// <summary>
    /// �~�b�V������j������֐�
    /// </summary>
    /// <param name="misson">�j������~�b�V����</param>
    public void RemoveMisson(IMissonBase misson);
}
