using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatas", menuName = "ScriptableObjects/WeaponDatasObject", order = 1)]
public class WeaponScriptableObjects : ScriptableObject
{
    [SerializeField, Tooltip("�e����̃f�[�^")]
    WeaponDataEntity[] _weaponDatas;

    public WeaponDataEntity[] WeaponDatas => _weaponDatas;
}

[System.Serializable]
public class WeaponDataEntity
{
    [SerializeField, Header("����̃^�C�v")]
    public WeaponType Type = WeaponType.NONE;
    [SerializeField, Header("����̕����U����")]
    float[] _rigitPower = new float[2];
    [SerializeField, Header("����̉��U����")]
    float[] _firePower = new float[2];
    [SerializeField, Header("����̗��U����")]
    float[] _elekePower = new float[2];
    [SerializeField, Header("����̕X���U����")]
    float[] _frozenPower = new float[2];
    [SerializeField, Header("���ߍU���̂��ߎ���")]
    public float[] _chargeLevels = new float[2] { 1f, 3f };

    public float[] RigitPower => _rigitPower;
    public float[] FirePower => _firePower;
    public float[] ElekePower => _elekePower;
    public float[] FrozenPower => _frozenPower;
    public float[] ChargeLevels => _chargeLevels;
}


