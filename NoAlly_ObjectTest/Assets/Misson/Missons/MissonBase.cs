using UnityEngine;

[System.Serializable]

public abstract class MissonBase : IMissonBase
{
    [SerializeField,Tooltip("ミッションのクリア判定")]
    bool _missonClear = false;
    
    [Tooltip("ミッションID")]
    int _missonID = -1;
    [Tooltip("ミッションの名前")]
    string _missonName = null;
    [Tooltip("ミッションの説明")]
    string _missonExplan = null;
    [Tooltip("ミッションの解放深度")]
    int _missonDepth = -1;

    public bool MissonClear => _missonClear;
    public int MissonID => _missonID;
    public string MissonName => _missonName;
    public string MissonExplan => _missonExplan;
    public int MissonDepth => _missonDepth;

    public MissonBase(int missonID,string missonName,string missonExplan,int missonDepth)
    {
        _missonID = missonID;
        _missonName = missonName;
        _missonExplan = missonExplan;
        _missonDepth = missonDepth;
    }

    /// <summary>
    /// ミッションのクリア判定処理
    /// </summary>
    public abstract void JudgeMissonClear();
}
