/// <summary>
/// �V�[�g���̓o�^�p
/// </summary>
public enum MissonType
{
    DeleteEnemy,
    CollectItem,
}
public enum ButtonState : int
{
    NONE, //�ʏ�
    SELECTED, //�I��
    DISIDED //����ς�
}

public enum CommandType : int
{
    NONE = -1,
    MAINWEAPON = 0, //���C������
    SUBWEAPON = 1, //�T�u����
    ELEMENT = 2, //����
    SKILL //�X�L��
}
