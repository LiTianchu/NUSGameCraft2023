using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroveHewer : Artifact
{
    [SerializeField]
    private float multiplier;
    public override int GetEffect()
    {
        if(WorldContext is ResourceWorld)
        {
            ResourceWorld w = (ResourceWorld)this.WorldContext;
            if (w.ResourceType == ResourceType.Wood)
            {
                return (int)(BasicValue*multiplier);
            }
        }
        
        return 0;
    }
}
