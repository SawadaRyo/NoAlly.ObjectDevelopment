
public interface IMissonBase
{
    /// <summary>
    /// �~�b�V�����̃N���A����
    /// </summary>
    public bool MissonClear { get; }
    /// <summary>
    /// �~�b�V�����̎��
    /// </summary>
    public MissonType MissonType { get; }
    /// <summary>
    /// �~�b�V������
    /// </summary>
    public string MissonName { get; }
    /// <summary>
    /// �~�b�V��������
    /// </summary>
    public string MissonExplan { get; }
    /// <summary>
    /// �~�b�V�������������U���[�x
    /// </summary>
    public int MissonDepth { get; }
}
