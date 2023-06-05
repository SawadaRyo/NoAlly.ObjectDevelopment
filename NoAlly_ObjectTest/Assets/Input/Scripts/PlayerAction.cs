//日本語コメント可
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAction
{
    public static void Attack(Animator animator,WeaponType weaponType)
    {
        animator.SetTrigger(weaponType.ToString());
    }
}
