using Unity.VisualScripting;
using UnityEngine;

public class AdventurerFactory : Factory
{
    [SerializeField] 
    private Adventurer adventurerPrefab;

    public IProduct GetProduct(Soul soul)
    {
        Adventurer adventurer = Instantiate(adventurerPrefab);
        adventurer.Soul = soul;
        adventurer.Artifact = new BasicArtifact();
        adventurer.Initialize();
        return adventurer;
    }
}
