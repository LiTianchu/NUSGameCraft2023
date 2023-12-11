using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private ResourceType generatingResourceType;
    [SerializeField]
    private float resourceGeneratingCD;
    [SerializeField]
    private int baseResourceQty;
    [SerializeField]
    private TMP_Text adventurerNumDisplay;
    

    private float _secPassed;
    private HashSet<Adventurer> _adventurers;

    public ResourceType ResourceType { get => generatingResourceType; set => generatingResourceType = value;}

    private void Start()
    {
        _adventurers = new HashSet<Adventurer>();
        IArtifact baseArtifact = new BasicArtifact();

        
    }
    private void Update()
    {
        _secPassed += Time.deltaTime;

        if(_secPassed > resourceGeneratingCD) 
        {
            //generate resource
            _secPassed = 0;
            GenerateResource();
        }

        adventurerNumDisplay.text = _adventurers.Count.ToString();

    }

    private void GenerateResource()
    {
        
        foreach(Adventurer adv in _adventurers)
        {
            ResourceManager.Instance.AddResource(baseResourceQty, generatingResourceType);
            
            adv.Artifact.UseEffect(this); //use artifact decorator effect
        }
        
    }

    public void AddAdventurer(Adventurer adv)
    {
        adv.transform.SetParent(transform);
        _adventurers.Add(adv);
        adv.OnAdventurerDead += RemoveAdventurer;
    }

    private void RemoveAdventurer(Adventurer adv)
    {
        _adventurers.Remove(adv);
        adv.OnAdventurerDead -= RemoveAdventurer;
        Destroy(adv.gameObject);
    }
}
