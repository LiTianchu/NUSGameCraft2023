using System;
using UnityEngine;

public class Adventurer : MonoBehaviour, IProduct
{
    private Soul _soul;
    private IArtifact _artifact;
    private float _lifeTime;
    private int _workPower;
    private World _world;

    public Soul Soul { get { return _soul; } set { _soul = value; } }
    public IArtifact Artifact {  get { return _artifact; } set { _artifact = value; } }
    public float LifeTime { get { return _lifeTime; } set { _lifeTime = value; } }
    public int WorkPower { get { return _workPower; } set { _workPower = value; } }
    public World World { get { return _world; } set { _world = value; } }
    public event Action<Adventurer> OnAdventurerDead;

    public void Initialize()
    {
        
    }

    private void Update()
    {
        if (_lifeTime < 0) //adventurer dies, it will be sent back to the pool
        {
            GrowAbility();
            OnAdventurerDead?.Invoke(this);
            
        }
        _lifeTime -= Time.deltaTime;
    }

    private void GrowAbility()
    {
        _soul.PowerGrowth = UnityEngine.Random.Range(1,4);
        _soul.HealthGrowth = UnityEngine.Random.Range(2, 5);
        _soul.Grow();
    }
    


}
