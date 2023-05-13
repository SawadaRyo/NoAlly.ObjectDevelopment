using UnityEngine;

[CreateAssetMenu(fileName = "MissonData", menuName = "ScriptableObjects/Misson")]

[System.Serializable]

public class MissonBase : ScriptableObject,IMissonBase
{
    [SerializeField,Tooltip("ミッションの名前")]
    string _missonName = null;
    [SerializeField,Tooltip("ミッションの説明")]
    string _missonExplan = null;
    [SerializeField,Tooltip("ミッションの系統")]
    int _missonDepth;

    [Tooltip("ミッションの解放判定")]
    bool _missonEnabled = false;
    [Tooltip("ミッションのクリア判定")]
    bool _missonClear = false;

    public bool MissonEnabled => _missonEnabled;
    public bool MissonClear => _missonClear;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;
}
