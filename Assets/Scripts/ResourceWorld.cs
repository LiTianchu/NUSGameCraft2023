using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceWorld : World
{
    [SerializeField]
    private ResourceType generatingResourceType;
    [SerializeField]
    private float resourceGeneratingCD;
    [SerializeField]
    private int baseResourceQty;

    protected float _nextResourceGeneratingTime;
    public ResourceType ResourceType { get => generatingResourceType; set => generatingResourceType = value; }
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _nextResourceGeneratingTime = resourceGeneratingCD;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (_secPassed > _nextResourceGeneratingTime)
        {
            //generate resource
            _nextResourceGeneratingTime = _secPassed + resourceGeneratingCD;
            GenerateResource();
        }

    }

    private void GenerateResource()
    {

        foreach (Adventurer adv in _adventurers)
        {
            ResourceManager.Instance.AddResource(baseResourceQty, generatingResourceType);

            adv.Artifact.UseEffect(this); //use artifact decorator effect
        }

    }
}
