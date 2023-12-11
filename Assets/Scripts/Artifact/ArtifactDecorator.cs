using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactDecorator : IArtifact
{
    protected IArtifact decoratedArtifact;

    public ArtifactDecorator(IArtifact decoratedArtifact)
    {
        this.decoratedArtifact = decoratedArtifact;
    }

    public virtual void UseEffect(World w)
    {
        decoratedArtifact.UseEffect(w);
    }
}
