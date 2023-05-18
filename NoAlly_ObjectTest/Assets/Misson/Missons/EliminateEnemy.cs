using UnityEngine;

public class EliminateEnemy : MissonBase
{
    [SerializeField,Tooltip("“G‚Ì‘”")]
    int targetEnemyCount = 0;

    public EliminateEnemy(int missonID, string missonName, string missonExplan, int missonDepth)
        : base(missonID, missonName, missonExplan, missonDepth)
    {
        
    }

    public override void JudgeMissonClear()
    {
        
    }
}
