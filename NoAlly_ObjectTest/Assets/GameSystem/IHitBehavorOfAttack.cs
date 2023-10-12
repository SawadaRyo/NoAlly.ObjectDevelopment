using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitBehavorOfAttack
{
    public void BehaviorOfHit(ObjectPowerValue damageValue, ElementType type);
    public void BehaviorOfHit(float[] damageValue, ElementType type);
}
