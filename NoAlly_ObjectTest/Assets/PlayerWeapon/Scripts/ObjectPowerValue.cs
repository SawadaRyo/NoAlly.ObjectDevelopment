using UnityEngine;

[System.Serializable]
public struct ObjectPowerValue
{
    [Range(0f, 2f),Header("オブジェクトの物理攻撃力")]
    public float defaultPower;
    [Range(0f, 2f), Header("オブジェクトの属性攻撃力")]
    public float elementPower;
    [Header("オブジェクトの属性")]
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

[System.Serializable]
public struct ObjectDefenceValue
{
    [Range(0f, 2f), Header("オブジェクトの防御力")]
    public float defencePower;
    [Header("防御力の属性")]
    public ElementType elementType;

    static readonly ObjectPowerValue zeroPower = new(0, 0, ElementType.RIGIT);

    public static ObjectPowerValue zero => zeroPower;

    public ObjectDefenceValue(float defencePower, ElementType elementType)
    {
        this.defencePower = defencePower;
        this.elementType = elementType;
    }
}
