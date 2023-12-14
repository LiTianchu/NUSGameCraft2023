using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeArtifact : Artifact
{
    [SerializeField]
    private float multiplier;
    public override int GetEffect()
    {
        return (int)(BasicValue*multiplier);
    }

}
