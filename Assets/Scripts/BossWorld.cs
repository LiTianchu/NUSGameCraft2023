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
        foreach(Adventurer adv in _adventurers)
        {
            rebellion += adv.WorkPower + rebellionPerAdventurer;
            Debug.Log(rebellion);
        }
        ResourceManager.Instance.RebellionQty = rebellion;
    }
}
