//日本語コメント可
using UnityEngine;

[CreateAssetMenu(fileName = "CannonParamater", menuName = "ScriptableObjects/ActorParamater/Enemy/Cannon", order = 1)]

public class CannonEnemyParamater : EnemyParamaterBase
{
    [SerializeField,Header("索敵中の右回転")]
    public Vector3 rotateRight = new Vector3(0f,90f,0f);
    [SerializeField, Header("索敵中の左回転")]
    public Vector3 rotateLeft = new Vector3(0f,-90f,0f);

    [SerializeField, Range(0f, 360f), Header("Z軸回転の最低角度")]
    public float minRotValueShaftX = 0f;
    [SerializeField, Range(0f, 360f), Header("Z軸回転の最高角度")]
    public float maxRotValueShaftX = 0f;
}
