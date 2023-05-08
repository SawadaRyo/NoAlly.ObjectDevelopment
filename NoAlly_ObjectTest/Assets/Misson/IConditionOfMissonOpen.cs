using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditionOfMissonOpen
{
    /// <summary>
    /// 解放条件の判定を行う関数
    /// </summary>
    /// <returns>解放されているか</returns>
    public bool ConditionOpen();
}
