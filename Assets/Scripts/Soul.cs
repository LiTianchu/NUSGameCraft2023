using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour, IProduct
{
    [SerializeField]
    private Sprite maleSprite;
    [SerializeField]
    private Sprite femaleSprite;

    [SerializeField]
    private GenderType gender;
    [SerializeField]
    private string soulName;
    [SerializeField]
    private IDeathReason deathReason;
    [SerializeField]
    private int power;
    [SerializeField]
    private int health;

    private int _powerGrowth;
    private int _healthGrowth;
    public GenderType Gender { get { return gender; } set {  gender = value; } }
    public string SoulName { get { return soulName; } set { soulName = value; } }
    public IDeathReason DeathReason { get { return deathReason; } set { deathReason = value; } }
    public int Power { get { return power; } set { power = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int PowerGrowth { get { return _powerGrowth; } set { _powerGrowth = value; } }
    public int HealthGrowth { get { return _healthGrowth; } set { _healthGrowth = value; } }
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    public void Initialize()
    {

    }

    public void SendAway()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateAppearance()
    {
        if (_spriteRenderer == null)
        {
            //lazy loading
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.sprite = gender == GenderType.Male? maleSprite : femaleSprite;
    }

    public void Grow()
    {
        power += _powerGrowth;
        health += _healthGrowth;
    }

}
