//日本語コメント可
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField]
    EnemyBase[] _enemyBase;

    // Start is called before the first frame update
    void Start()
    {
        foreach (EnemyBase enemyBase in _enemyBase)
        {
            enemyBase.DisactiveForInstantiate(null,true);
        }
    }
}
