using UnityEngine;

//[CreateAssetMenu()"Misson",order = 0)]

[System.Serializable]

public class MissonBase : ScriptableObject
{
    [SerializeField,Tooltip("�~�b�V�����̖��O")]
    string _missonName = null;
    [SerializeField,Tooltip("�~�b�V�����̐���")]
    string _missonExplan = null;
    [SerializeField,Tooltip("�~�b�V�����̌n��")]
    MissonType _missonType;

    [Tooltip("�~�b�V�����̉������")]
    bool _missonEnabled = false;

    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public MissonType MissonType => _missonType;
    public bool IsMissonEnabled => _missonEnabled;
}
