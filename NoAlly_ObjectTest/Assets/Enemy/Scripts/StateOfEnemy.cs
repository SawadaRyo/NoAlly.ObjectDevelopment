using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateOfEnemy : int
{
    None = -1,

    Idle = 0, //基本

    Move = 1, //移動

    Saerching = 2, //プレイヤーを捜索中

    BattlePosture = 3, //戦闘態勢

    Attack = 4, //攻撃
    NormalAttack = 41, //通常攻撃
    LoopAttack = 42, //継続的に繰り返す攻撃

    Death = 5 //死亡
}
