/// <summary>
/// �~�b�V�����̌n��
/// </summary>
public enum MissonDataPath
{
    DeleteEnemy,
    CollectItem,
}
/// <summary>
/// �{�^���̃X�e�[�g
/// </summary>
public enum ButtonState : int
{
    NONE, //�ʏ�
    SELECTED, //�I��
    DISIDED //����ς�
}
/// <summary>
/// �{�^���̌n��
/// </summary>
public enum CommandType : int
{
    NONE = -1,
    MAINWEAPON = 0, //���C������
    SUBWEAPON = 1, //�T�u����
    ELEMENT = 2, //����
    SKILL //�X�L��
}
