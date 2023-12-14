using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceArtifact : Artifact
{
    [SerializeField]
    private ResourceType resourceType;
    [SerializeField]
    private float multiplier;
    public override int GetEffect()
    {
        if(WorldContext is ResourceWorld)
        {
            ResourceWorld w = (ResourceWorld)this.WorldContext;
            if (w.ResourceType == resourceType)
            {
                return (int)(BasicValue*multiplier);
            }
        }
        
        return 0;
    }
}


