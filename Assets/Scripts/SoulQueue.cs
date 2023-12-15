using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulQueue : Singleton<SoulQueue>
{
    //[SerializeField]
    //private Soul soulPrefab;
    [SerializeField]
    private Vector2 firstSoulPos;
    [SerializeField]
    private Vector2 soulPositionOffset;
    [SerializeField]
    private Vector3 soulScaleDifference;
    [SerializeField]
    private Vector3 soulCreationPos;
    [SerializeField]
    private AdventurerFactory adventurerFactory;
    [SerializeField]
    private SoulFactory soulFactory;
    [SerializeField]
    private TMP_Text soulNameDisplay;
    [SerializeField]
    private Image soulGenderDisplay;
    [SerializeField]
    private TMP_Text soulDeathReasonDisplay;
    [SerializeField]
    private TMP_Text soulPowerDisplay;
    [SerializeField]
    private TMP_Text soulHealthDisplay;
    [SerializeField]
    private TMP_Text soulInQueueNumDisplay;
    [SerializeField]
    private TMP_Text soulPopulationDisplay;
    [SerializeField]
    private AddCapacity addCapacityBtn;
    [SerializeField]
    private Sprite maleSymbol;
    [SerializeField]
    private Sprite femaleSymbol;
    

    private Queue<Soul> _souls;
    private Soul _currentSoul;
    private int _maxSoulPopulation=5;
    private int _currentSoulPopulation;

    private static readonly int SOUL_COST = 100;
    
    public Vector2 FirstSouldPos { get => firstSoulPos; }

    public event Action OnSoulChanged;

    private void Start()
    {
        InitializeQueue();
    }

    public void InitializeQueue()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //generate 4 souls at the start
        _souls = new Queue<Soul>();
        for (int i = 0; i < 4; i++)
        {
            CreateNewSoul((GenderType)(i % 2));
        }
        
        ProgressQueue();
        //NextSoul();
    }

    public void NextSoul()
    {
        //set current soul as the first soul in the queue
        if(_souls.Count == 0)
        {
            Debug.LogWarning("No more souls in queue"); 
            return;
        }
        _currentSoul = _souls.Dequeue();
        ProgressQueue();
    }

    public void UpdateSoulInfo() { 
        if (_currentSoul != null)
        {
            soulNameDisplay.text = _currentSoul.SoulName;

            soulGenderDisplay.gameObject.SetActive(true);
            if (_currentSoul.Gender == GenderType.Male)
            {
                soulGenderDisplay.sprite = maleSymbol;
            }
            else
            {
                soulGenderDisplay.sprite = femaleSymbol;
            }
            soulGenderDisplay.SetNativeSize();

            soulPowerDisplay.text = _currentSoul.Power.ToString();
            soulHealthDisplay.text = _currentSoul.Health.ToString();

            if(_currentSoul.PowerGrowth > 0)
            {
                soulPowerDisplay.text += " (+" + _currentSoul.PowerGrowth + ")";
            }

            if (_currentSoul.HealthGrowth > 0)
            {
                soulHealthDisplay.text += " (+" + _currentSoul.HealthGrowth + ")";
            }

        }

    }

    public void ClearSoulInfo()
    {
        soulNameDisplay.text = "";
        soulGenderDisplay.gameObject.SetActive(false);
        soulDeathReasonDisplay.text = "";
        soulPowerDisplay.text = "";
        soulHealthDisplay.text = "";
    }

    private void ProgressQueue()
    {
        UpdateSoulInfo();
        //move up the curr soul to the front
        _currentSoul.Destination = firstSoulPos;
        _currentSoul.transform.localScale = Vector3.one;

        soulInQueueNumDisplay.text = _souls.Count + " More Souls Waiting";

        if (_souls.Count == 0)
        {
            return;
        }

        //move the next soul to the curr soul's back
        Soul soulAfter = _souls.Peek();
        soulAfter.Destination = firstSoulPos + soulPositionOffset;
    }

    public void AddSoul(Soul soul)
    {
        soul.transform.SetParent(transform); //set as child of soul queue
        if (_currentSoul == null) //if no current soul, set as current soul, do not add to queue
        {
            _currentSoul = soul;

            ProgressQueue();
            return;
        }

        _souls.Enqueue(soul); //add to queue
        
        if (_souls.Count == 1) //set display position and scale accordingly
        {
            soul.Destination = firstSoulPos + soulPositionOffset;
        }
        else
        {
            soul.Destination = firstSoulPos + soulPositionOffset * 2;
        }

        soulInQueueNumDisplay.text = _souls.Count + " More Souls Waiting";
        
    }

    private void CreateNewSoul(GenderType gender)
    {
        

        Soul soul = (Soul)soulFactory.GetProduct(gender);
        soul.transform.localPosition = soulCreationPos;
        _currentSoulPopulation++;
        soulPopulationDisplay.text = _currentSoulPopulation + "/" + _maxSoulPopulation;
        AddSoul(soul);
    }

    public void BuyNewSoul()
    {
        if (_currentSoulPopulation >= _maxSoulPopulation)
        {
            //Debug.LogWarning("Max soul population reached");
            WarningToast.Instance.ShowToast("More soul capacity is required!");
            return;
        }

        if (ResourceManager.Instance.RemoveResource(0,0,SOUL_COST,SOUL_COST))
        {
            CreateNewSoul((GenderType)UnityEngine.Random.Range(0, 2));
        }
        //else
        //{
        //    //Debug.Log("Not enough resources");
        //    WarningToast.Instance.ShowToast("Not enough resources!");
        //}
    }

    public void AddCapacity()
    {
        if (ResourceManager.Instance.RemoveResource(addCapacityBtn.WoodCost,addCapacityBtn.RockCost,0,0))
        {
            _maxSoulPopulation += 5;
            addCapacityBtn.AddWoodCost();
            addCapacityBtn.AddStoneCost();
            soulPopulationDisplay.text = _currentSoulPopulation + "/" + _maxSoulPopulation;
        }
    }

    public Soul GetCurrentSoul()
    {
        return _currentSoul;
    }

    public void SendToWorld(World world)
    {
        if(_currentSoul == null)
        {
            Debug.LogWarning("No more souls in queue");
            return;
        }

        Adventurer adventurer = adventurerFactory.GetProduct(_currentSoul, world, _currentSoul.Health, _currentSoul.Power) as Adventurer; //create afventurer

        adventurer.OnAdventurerDead += HandleAdventurerDead;
        
        ArtifactSelector.Instance.ClearAllSlots(); //clear all artifact slots
        world.AddAdventurer(adventurer);

        //send away the current soul
        if (_currentSoul != null)
        {
            _currentSoul.SendAway(world);
            _currentSoul = null;
            ClearSoulInfo();
        }

        NextSoul();

    }

    private void HandleAdventurerDead(Adventurer adventurer)
    {
        Soul soul = adventurer.Soul;
        soul.DestinationWorld = null;
        //activate gameobject of the soul to display it
        soul.gameObject.SetActive(true); 

        AddSoul(soul); //add soul back to queue
        adventurer.OnAdventurerDead -= HandleAdventurerDead;
    }
}
