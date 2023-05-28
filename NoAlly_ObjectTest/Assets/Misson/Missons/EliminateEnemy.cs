using UnityEngine;

public class EliminateEnemy : MissonBase
{
    [SerializeField,Tooltip("“G‚Ì‘”")]
    int targetEnemyCount = 0;

    public EliminateEnemy(int missonID, string missonName, string missonExplan)
        : base(missonID, missonName, missonExplan)
    {
        
    }

    public override void JudgeMissonClear()
    {
        
    }
}
