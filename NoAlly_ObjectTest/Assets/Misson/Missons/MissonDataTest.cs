using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class MissonDataTest : MonoBehaviour
{
    [SerializeField] DataReader _reader;
    [SerializeField] int _dataDepth = 0;
    [SerializeField] MissonDataPath _dataPath = 0;

    async void Start()
    {
        await _reader.Reload();
    }
    public void MissonEvent(DataReader datas)
    {
        string[] target = datas.Sheet[(int)_dataPath].DatasList[_dataDepth];
        //for(int i = 0; i < target.Length; i++)
        //{
            Debug.Log(target);
        //}
    }
}
