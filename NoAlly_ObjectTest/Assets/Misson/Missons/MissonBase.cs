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

    public bool MissonClear => _missonClear;
    public int MissonID => _missonID;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;

    public MissonBase(int missonID,string missonName,string missonExplan)
    {
        _missonID = missonID;
        _missonName = missonName;
        _missonExplan = missonExplan;
    }

    /// <summary>
    /// �~�b�V�����̃N���A���菈��
    /// </summary>
    public abstract void JudgeMissonClear();
}
