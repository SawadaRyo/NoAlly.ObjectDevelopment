using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectObject
{
    /// <summary>
    /// このオブジェクトの親関係
    /// </summary>
    public SelectObjecArray Perent { get; }
    /// <summary>
    /// ゲーム実行時初期化
    /// </summary>
    public void Initialize(SelectObjecArray perent);

    /// <summary>
    /// 選択時実行関数
    /// </summary>
    /// <param name="isSelect"></param>
    public void Selected(bool isSelect);
}
