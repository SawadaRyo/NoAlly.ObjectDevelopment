//日本語コメント可
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputState<TOwner> where TOwner : Object
{
    public void ActorAnimationState(TOwner owner);
}
