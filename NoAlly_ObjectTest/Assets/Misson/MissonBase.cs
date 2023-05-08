using UnityEngine;

//[CreateAssetMenu()"Misson",order = 0)]

[System.Serializable]

public class MissonBase : ScriptableObject
{
    [SerializeField,Tooltip("ミッションの名前")]
    string _missonName = null;
    [SerializeField,Tooltip("ミッションの説明")]
    string _missonExplan = null;
    [SerializeField,Tooltip("ミッションの系統")]
    MissonType _missonType;

    [Tooltip("ミッションの解放判定")]
    bool _missonEnabled = false;

    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public MissonType MissonType => _missonType;
    public bool IsMissonEnabled => _missonEnabled;
}
