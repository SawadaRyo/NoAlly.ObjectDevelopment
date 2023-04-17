using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectObject
{
    /// <summary>
    /// ゲーム実行時初期化
    /// </summary>
    public void Initialize();

    /// <summary>
    /// 選択時実行関数
    /// </summary>
    /// <param name="isSelect"></param>
    public void Selected(bool isSelect);

    /// <summary>
    /// 決定時実行関数
    /// </summary>
    public void Disaide();

    /// <summary>
    /// 決定時実行関数
    /// </summary>
    /// <param name="isDisaide">決定判定</param>
    public void Disaide(bool isDisaide);

    /// <summary>
    /// コンポーネント展開時に呼ぶ関数
    /// </summary>
    public void Extended();

    /// <summary>
    /// コンポーネント縮小時に呼ぶ関数
    /// </summary>
    public void Closed();
}
