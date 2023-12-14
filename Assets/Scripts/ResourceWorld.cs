using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceWorld : World
{
    [SerializeField]
    private ResourceType generatingResourceType;
    [SerializeField]
    private float resourceGeneratingCD;

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
        if (GameManager.Instance.TimePassed > _nextResourceGeneratingTime)
        {
            //generate resource
            _nextResourceGeneratingTime = GameManager.Instance.TimePassed + resourceGeneratingCD;
            GenerateResource();
        }

    }

    private void GenerateResource()
    {
        int resourceQty = 0;
        foreach (Adventurer adv in _adventurers)
        {
            resourceQty += adv.WorkPower;

            if (adv.Artifacts == null)
            {
                continue;
            }

            foreach (Artifact artifact in adv.Artifacts)
            {
                if (artifact is ResourceArtifact)
                {
                    artifact.WorldContext = this;
                    artifact.BasicValue = adv.WorkPower;
                    resourceQty += artifact.UseEffect(); //use artifact effect
                }
            }

        }
        ResourceManager.Instance.AddResource(resourceQty, generatingResourceType);

    }
}
