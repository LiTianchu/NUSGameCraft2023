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

    public GenderType Gender { get { return gender; } set {  gender = value; } }
    public string SoulName { get { return soulName; } set { soulName = value; } }
    public IDeathReason DeathReason { get { return deathReason; } set { deathReason = value; } }

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

}
