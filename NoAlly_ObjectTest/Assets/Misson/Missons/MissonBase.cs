using UnityEngine;

[CreateAssetMenu(fileName = "MissonData", menuName = "ScriptableObjects/Misson")]

[System.Serializable]

public class MissonBase : ScriptableObject,IMissonBase
{
    [SerializeField,Tooltip("�~�b�V�����̖��O")]
    string _missonName = null;
    [SerializeField,Tooltip("�~�b�V�����̐���")]
    string _missonExplan = null;
    [SerializeField,Tooltip("�~�b�V�����̌n��")]
    int _missonDepth;

    [Tooltip("�~�b�V�����̉������")]
    bool _missonEnabled = false;
    [Tooltip("�~�b�V�����̃N���A����")]
    bool _missonClear = false;

    public bool MissonEnabled => _missonEnabled;
    public bool MissonClear => _missonClear;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;
}
