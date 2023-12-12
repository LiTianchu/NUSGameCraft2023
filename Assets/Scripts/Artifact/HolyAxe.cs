using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAxe : ArtifactDecorator
{
    public HolyAxe(IArtifact artifact) : base(artifact)
    {

    }

    public override void UseEffect(World w)
    {
        base.UseEffect(w);
        AddHolyAxeEffect(w);
    }

    private void AddHolyAxeEffect(World w)
    {
        Debug.Log("Holy Axe Effect");
        if(w is ResourceWorld)
        {
            if (((ResourceWorld)w).ResourceType == ResourceType.Wood)
            {
                ResourceManager.Instance.AddResource(2, ResourceType.Wood);
            }
        }
        
    }
}
