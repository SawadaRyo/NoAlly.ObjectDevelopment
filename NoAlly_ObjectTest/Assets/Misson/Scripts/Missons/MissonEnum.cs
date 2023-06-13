/// <summary>
/// ミッションの系統
/// </summary>
public enum MissonDataPath
{
    DeleteEnemy,
    CollectItem,
}
/// <summary>
/// ボタンのステート
/// </summary>
public enum ButtonState : int
{
    NONE, //通常
    SELECTED, //選択中
    DISIDED //決定済み
}
/// <summary>
/// ボタンの系統
/// </summary>
public enum CommandType : int
{
    NONE = -1,
    MAINWEAPON = 0, //メイン武器
    SUBWEAPON = 1, //サブ武器
    ELEMENT = 2, //属性
    SKILL //スキル
}
