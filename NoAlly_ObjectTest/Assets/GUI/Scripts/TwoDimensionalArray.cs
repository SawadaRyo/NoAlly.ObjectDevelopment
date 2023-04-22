using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DimensionalArray
{
    
    //�V���A���C�Y���ꂽ�q�v�f�N���X
    [System.Serializable]
    public class GenericArray<T>
    {
        [SerializeField]
        T[] _childArray;

        public T[] ChildArrays => _childArray;
    }
}
