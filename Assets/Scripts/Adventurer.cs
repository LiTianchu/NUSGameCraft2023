using System;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour, IProduct
{
    private Soul _soul;
    private List<Artifact> _artifacts;
    private float _lifeTime;
    private int _workPower;
    private World _world;

    public Soul Soul { get { return _soul; } set { _soul = value; } }
    public List<Artifact> Artifacts {  get { return _artifacts; } set { _artifacts = value; } }
    public float LifeTime { get { return _lifeTime; } set { _lifeTime = value; } }
    public int WorkPower { get { return _workPower; } set { _workPower = value; } }
    public World World { get { return _world; } set { _world = value; } }
    public event Action<Adventurer> OnAdventurerDead;

    private float _timeOfDeath;
    public void Initialize()
    {
        _timeOfDeath = SimulatorManager.Instance.TimePassed + _lifeTime;
    }

    private void Update()
    {
        if (SimulatorManager.Instance.TimePassed > _timeOfDeath) //adventurer dies, it will be sent back to the pool
        {
            GrowAbility();
            OnAdventurerDead?.Invoke(this);
            
        }
        
    }

    private void GrowAbility()
    {
        _soul.PowerGrowth = UnityEngine.Random.Range(1,4);
        _soul.HealthGrowth = UnityEngine.Random.Range(2, 5);
        _soul.Grow();
    }
    


}
