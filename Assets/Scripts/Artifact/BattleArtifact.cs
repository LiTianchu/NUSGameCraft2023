using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArtifact : Artifact
{
    [SerializeField]
    private float multiplier;
    public override int GetEffect()
    {
        if(WorldContext is BossWorld)
        {
            BossWorld w = (BossWorld)this.WorldContext;
            return (int)(BasicValue*multiplier);
        }
        
        return 0;
    }
}


