using System.Collections.Generic;
using UnityEngine;

public class MissonManager : MonoBehaviour, IMissonManager
{
    [SerializeField, Tooltip("�~�b�V�����f�[�^��ۑ����Ă���CSV�̃p�X")]
    string _dataPath = "";

    [Tooltip("")]
    List<IMissonBase> _missons = new();

    public List<IMissonBase> MissonBases => _missons;

    public void MissonStart()
    {
        
    }

    public void MissonClear()
    {
        
    }

    public void AddMisson(IMissonBase misson)
    {
        _missons.Add(misson);
    }

    public void RemoveMisson(IMissonBase misson)
    {
        _missons.Remove(misson);
    }
}
