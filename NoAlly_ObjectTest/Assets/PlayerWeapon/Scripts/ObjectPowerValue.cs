using UnityEngine;

[System.Serializable]
public struct ObjectPowerValue
{
    [Range(0f, 2f),Header("�I�u�W�F�N�g�̕����U����")]
    public float defaultPower;
    [Range(0f, 2f), Header("�I�u�W�F�N�g�̑����U����")]
    public float elementPower;
    [Range(0f, 2f), Header("�I�u�W�F�N�g�̑���")]
    public ElementType elementType;

    static readonly ObjectPowerValue zeroPower = new(0, 0, ElementType.RIGIT);

    public static ObjectPowerValue zero => zeroPower;

    public ObjectPowerValue(float defaultPower, float elementPower, ElementType elementType)
    {
        this.defaultPower = defaultPower;
        this.elementPower = elementPower;
        this.elementType = elementType;
    }
}
