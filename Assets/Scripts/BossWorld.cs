using Unity.VisualScripting;
using UnityEngine;

public class BossWorld : World
{
    [SerializeField]
    private int rebellionPerAdventurer = 100;

    public override void Start()
    {
        base.Start();

    }
    public override void Update()
    {
        base.Update();
        int rebellion = 0;
        foreach (Adventurer adv in _adventurers)
        {
            rebellion += adv.WorkPower;
            foreach (Artifact artifact in adv.Artifacts)
            {
                artifact.WorldContext = this;
                artifact.BasicValue = adv.WorkPower;
                if (artifact is BattleArtifact)
                {
                    rebellion += artifact.UseEffect();
                }
               
            }
            Debug.Log(rebellion);
        }
        ResourceManager.Instance.RebellionQty = rebellion;
    }

    public override void OnAdventurerEnter(Adventurer adv)
    {
        foreach (Artifact artifact in adv.Artifacts)
        {

            if (artifact is Excalibur)
            {
                Debug.Log("Excalibur");
                GameManager.Instance.AddDestruction(artifact.UseEffect());
            }
        }
    }
}
