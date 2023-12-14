using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour, IProduct
{
    [SerializeField]
    private GameObject maleImage;
    [SerializeField]
    private GameObject femaleImage;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float minScaleSize;
    [SerializeField]
    private GenderType gender;
    [SerializeField]
    private string soulName;
    [SerializeField]
    private int power;
    [SerializeField]
    private int health;

    private int _powerGrowth;
    private int _healthGrowth;
    public GenderType Gender { get { return gender; } set {  gender = value; } }
    public string SoulName { get { return soulName; } set { soulName = value; } }
    //public IDeathReason DeathReason { get { return deathReason; } set { deathReason = value; } }
    public int Power { get { return power; } set { power = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int PowerGrowth { get { return _powerGrowth; } set { _powerGrowth = value; } }
    public int HealthGrowth { get { return _healthGrowth; } set { _healthGrowth = value; } }
    private SpriteRenderer _spriteRenderer;

    private Vector3 _destination;
    private World _destinationWorld;
    public Vector3 Destination { get { return _destination; } set { _destination = value; } }
    public World DestinationWorld { get { return _destinationWorld; } set { _destinationWorld = value; } }
    
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    private void Update()
    {
        
        //move to destination if curr position is not equal to destination
        if (transform.localPosition != _destination)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination, moveSpeed * Time.deltaTime);
        }

        //change size according to distance to first soul position
        //the closer to the first soul position, the bigger the soul
        if(this.transform.localScale.x >= minScaleSize)
        {
            float distanceToFirstSoulPos = Vector3.Distance(transform.localPosition, SoulQueue.Instance.FirstSouldPos);
            transform.localScale = Vector3.one * (1 - distanceToFirstSoulPos / (2 * _mainCamera.orthographicSize));
        }
        else
        {
            transform.localScale = Vector3.one * minScaleSize;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("World") && _destinationWorld!=null &&
            collision.transform.GetComponent<World>().Equals(_destinationWorld))
        {
            gameObject.SetActive(false);
        }
    }

    public void SendAway(World world)
    {
        //change from world position to local position
        _destinationWorld = world;
        _destination = transform.parent.InverseTransformPoint(world.transform.position);
    }

    public void UpdateAppearance()
    {
        if (_spriteRenderer == null)
        {
            //lazy loading
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        maleImage.SetActive(false);
        femaleImage.SetActive(false);

        if (gender == GenderType.Male) 
        { 
            maleImage.SetActive(true);
        }
        else 
        { 
            femaleImage.SetActive(true); 
        }
        
    }

    public void Grow()
    {
        power += _powerGrowth;
        health += _healthGrowth;
    }

}
