using UnityEngine;

[System.Serializable]

public abstract class MissonBase : IMissonBase
{
    [SerializeField,Tooltip("�~�b�V�����̃N���A����")]
    bool _missonClear = false;
    
    [Tooltip("�~�b�V����ID")]
    int _missonID = -1;
    [Tooltip("�~�b�V�����̖��O")]
    string _missonName = null;
    [Tooltip("�~�b�V�����̐���")]
    string _missonExplan = null;
    [Tooltip("�~�b�V�����̉���[�x")]
    int _missonDepth = -1;
    [Tooltip("�~�b�V�����̌n��")]
    MissonType _missonType;

    public bool MissonClear => _missonClear;
    public int MissonID => _missonID;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;
    public MissonType MissonType => _missonType;

    public MissonBase(int missonID,string missonName,string missonExplan,int missonDepth,MissonType missonType)
    {
        _missonID = missonID;
        _missonName = missonName;
        _missonExplan = missonExplan;
        _missonDepth = missonDepth;
        _missonType = missonType;
    }

    /// <summary>
    /// �~�b�V�����̃N���A���菈��
    /// </summary>
    public abstract void JudgeMissonClear();
}
