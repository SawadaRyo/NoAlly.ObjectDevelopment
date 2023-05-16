using UnityEngine;

[CreateAssetMenu(fileName = "MissonData", menuName = "ScriptableObjects/Misson")]

[System.Serializable]

public class MissonBase : ScriptableObject,IMissonBase
{
    [SerializeField,Tooltip("ミッションの名前")]
    string _missonName = null;
    [SerializeField,Tooltip("ミッションの説明")]
    string _missonExplan = null;
    [SerializeField, Tooltip("ミッションの系統")]
    int _missonDepth = 0;
    [SerializeField, Tooltip("ミッションの解放深度")]
    MissonType _missonType;
    
    [Tooltip("ミッションのクリア判定")]
    bool _missonClear = false;

    public bool MissonClear => _missonClear;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;
    public MissonType MissonType => _missonType;
}
