using System;
using UnityEngine;

public class Adventurer : MonoBehaviour, IProduct
{
    private Soul _soul;
    private IArtifact _artifact;
    private float _lifeTime;

    public Soul Soul { get { return _soul; } set { _soul = value; } }
    public IArtifact Artifact {  get { return _artifact; } set { _artifact = value; } }
    public float LifeTime { get { return _lifeTime; } set { _lifeTime = value; } }

    public event Action<Adventurer> OnAdventurerDead;

    public void Initialize()
    {
        
    }

    private void Update()
    {
        if (_lifeTime < 0)
        {
            OnAdventurerDead?.Invoke(this);
        }
        _lifeTime -= Time.deltaTime;
        
    }

    


}
