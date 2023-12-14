using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdventurerFactory : Factory
{
    [SerializeField] 
    private Adventurer adventurerPrefab;

    public IProduct GetProduct(Soul soul, World world, int health, int power)
    {
        Adventurer adventurer = Instantiate(adventurerPrefab);
        adventurer.Soul = soul;
        adventurer.Artifacts = new List<Artifact>();

        adventurer.LifeTime = health;
        foreach (ArtifactSlot slot in ArtifactSelector.Instance.ArtifactSlots)
        {
            Artifact artifact = slot.Artifact;
            if (artifact != null)
            {
                adventurer.Artifacts.Add(artifact);

                //apply life artifact effect
                if (artifact is LifeArtifact)
                {
                    artifact.WorldContext = world;
                    artifact.BasicValue = health;
                    adventurer.LifeTime += artifact.UseEffect();
                }
            }
        }

        adventurer.WorkPower = power; //work power
        adventurer.World = world;
        adventurer.Initialize();
        return adventurer;
    }
}
