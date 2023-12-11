using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private AdventurerFactory adventurerFactory;
    [SerializeField]
    private SoulFactory soulFactory;
    [SerializeField]
    private TMP_Text soulNameDisplay;
    [SerializeField]
    private TMP_Text soulGenderDisplay;
    [SerializeField]
    private TMP_Text soulDeathReasonDisplay;


    private Queue<Soul> _souls;
    private Soul _currentSoul;

    private int _currentSoulPopulation;

    private static readonly int MAX_SOUL_POPULATION = 50;
    private static readonly float SOUL_LIFE_TIME = 10f;

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
            Soul soul = (Soul)soulFactory.GetProduct((GenderType)(i%2));
            
            _currentSoulPopulation++;
            AddSoul(soul);
        }
        
        UpdateQueue();
        //NextSoul();
    }

    public void NextSoul()
    {
        //send away the current soul
        if(_currentSoul != null)
        {
            _currentSoul.SendAway();
            _currentSoul = null;
            ClearSoulInfo();
        }

        //set current soul as the first soul in the queue
        if(_souls.Count == 0)
        {
            Debug.LogWarning("No more souls in queue"); 
            return;
        }
        _currentSoul = _souls.Dequeue();
        UpdateQueue();

    }

    public void UpdateSoulInfo() { 
        if (_currentSoul != null)
        {
            soulNameDisplay.text = _currentSoul.SoulName;
            soulGenderDisplay.text = _currentSoul.Gender.ToString();
        }
    }

    public void ClearSoulInfo()
    {
        soulNameDisplay.text = "";
        soulGenderDisplay.text = "";
        soulDeathReasonDisplay.text = "";
    }

    private void UpdateQueue()
    {
        UpdateSoulInfo();
        //move up the curr soul to the front
        _currentSoul.transform.localPosition = firstSoulPos;
        _currentSoul.transform.localScale = Vector3.one;
        
        if(_souls.Count == 0)
        {
            return;
        }

        //move the next soul to the curr soul's back
        Soul soulAfter = _souls.Peek();
        soulAfter.transform.localPosition = firstSoulPos + soulPositionOffset;
        soulAfter.transform.localScale = _currentSoul.transform.localScale - soulScaleDifference;
    }

    public void AddSoul(Soul soul)
    {
        soul.transform.SetParent(transform); //set as child of soul queue
        if (_currentSoul == null) //if no current soul, set as current soul, do not add to queue
        {
            _currentSoul = soul;

            UpdateQueue();
            return;
        }

        _souls.Enqueue(soul); //add to queue
        
        if (_souls.Count == 1) //set display position and scale accordingly
        {
            soul.transform.localPosition = firstSoulPos + soulPositionOffset;
            soul.transform.localScale = Vector3.one - soulScaleDifference;
        }
        else
        {
            soul.transform.localPosition = firstSoulPos + soulPositionOffset * 2;
            soul.transform.localScale = Vector3.one - soulScaleDifference * 2;
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
        Adventurer adventurer = adventurerFactory.GetProduct(_currentSoul) as Adventurer; //create afventurer

        adventurer.OnAdventurerDead += HandleAdventurerDead;

        adventurer.LifeTime = SOUL_LIFE_TIME; //life time
        adventurer.Artifact = new HolyAxe(adventurer.Artifact); //add decorator
        world.AddAdventurer(adventurer);

        NextSoul();

    }

    private void HandleAdventurerDead(Adventurer adventurer)
    {
        Soul soul = adventurer.Soul;
        //activate gameobject to the soul
        soul.gameObject.SetActive(true);

        AddSoul(soul);
        adventurer.OnAdventurerDead -= HandleAdventurerDead;
    }
}
