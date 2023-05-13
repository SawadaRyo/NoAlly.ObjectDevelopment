
public interface IMissonBase
{
    /// <summary>
    /// ミッションの解放判定
    /// </summary>
    public bool MissonEnabled { get; }
    /// <summary>
    /// ミッションのクリア判定
    /// </summary>
    public bool MissonClear { get; }
    /// <summary>
    /// ミッション名
    /// </summary>
    public string MissonName { get; }
    /// <summary>
    /// ミッション説明
    /// </summary>
    public string MissonExplan { get; }
    /// <summary>
    /// ミッションを解放する攻略深度
    /// </summary>
    public int MissonDepth { get; }
}
