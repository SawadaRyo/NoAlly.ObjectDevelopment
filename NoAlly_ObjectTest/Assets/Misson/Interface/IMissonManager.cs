using System.Collections.Generic;

public interface IMissonManager
{
    /// <summary>
    /// ミッションリスト
    /// </summary>
    public List<IMissonBase> MissonBases { get; }
    /// <summary>
    /// ミッション開始時に呼ぶ関数
    /// </summary>
    public void MissonStart();
    /// <summary>
    /// ミッションクリア時に呼ぶ関数
    /// </summary>
    public void MissonClear();
    /// <summary>
    /// ミッションを受注する関数
    /// </summary>
    /// <param name="misson">追加するミッション </param>
    public void AddMisson(IMissonBase misson);
    /// <summary>
    /// ミッションを破棄する関数
    /// </summary>
    /// <param name="misson">破棄するミッション</param>
    public void RemoveMisson(IMissonBase misson);
}
