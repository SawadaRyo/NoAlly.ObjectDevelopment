using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;

public class MissonDataTest : MonoBehaviour
{
    [SerializeField] DataReader _reader;
    [SerializeField] int _dataDepth = 0;
    [SerializeField] MissonDataPath _dataPath = 0;

    GSSChangeToCSV _csv = new();
    string _dataFilePath = "";

    async void Start()
    {
        _dataFilePath = $"Assets/Misson/ServarData/MissonCSV/" + _dataPath.ToString() + ".csv";
        await _reader.Reload();
        if (!File.Exists(_dataFilePath))
        {
            _csv.CreateCSV(_dataFilePath, _reader.Sheet[0].DatasList[0]);
        }
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
