using UnityEngine;

[CreateAssetMenu(fileName = "MissonData", menuName = "ScriptableObjects/Misson")]

[System.Serializable]

public class MissonBase : ScriptableObject,IMissonBase
{
    [SerializeField,Tooltip("�~�b�V�����̖��O")]
    string _missonName = null;
    [SerializeField,Tooltip("�~�b�V�����̐���")]
    string _missonExplan = null;
    [SerializeField, Tooltip("�~�b�V�����̌n��")]
    int _missonDepth = 0;
    [SerializeField, Tooltip("�~�b�V�����̉���[�x")]
    MissonType _missonType;
    
    [Tooltip("�~�b�V�����̃N���A����")]
    bool _missonClear = false;

    public bool MissonClear => _missonClear;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;
    public MissonType MissonType => _missonType;
}
