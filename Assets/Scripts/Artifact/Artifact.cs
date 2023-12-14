using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Artifact : MonoBehaviour, IArtifact
{
    [SerializeField]
    private string artifactName;
    [SerializeField]
    private string artifactDescription;
    [SerializeField]
    private Sprite artifactSprite;
    [SerializeField]
    private TMP_Text woodCostDisplay;
    [SerializeField]
    private TMP_Text rockCostDisplay;
    [SerializeField]
    private TMP_Text crystalCostDisplay;
    [SerializeField]
    private TMP_Text waterCostDisplay;

    [Header("Costs")]
    [SerializeField]
    private int woodCost;
    [SerializeField]
    private int rockCost;
    [SerializeField]
    private int crystalCost;
    [SerializeField]
    private int waterCost;

    private World _worldContext;
    private int _basicValue;
    public World WorldContext { get { return _worldContext; } set { _worldContext = value; }}
    public int BasicValue { get => _basicValue; set => _basicValue = value; }
    public Sprite ArtifactSprite { get => artifactSprite; set => artifactSprite = value; }
    public int WoodCost { get => woodCost; set => woodCost = value; }
    public int RockCost { get => rockCost; set => rockCost = value; }
    public int CrystalCost { get => crystalCost; set => crystalCost = value; }
    public int WaterCost { get => waterCost; set => waterCost = value; }

    private void Start()
    {
        woodCostDisplay.text = woodCost.ToString();
        rockCostDisplay.text = rockCost.ToString();
        crystalCostDisplay.text = crystalCost.ToString();
        waterCostDisplay.text = waterCost.ToString();
    }
    public int UseEffect()
    {
        return GetEffect();
    }

    public abstract int GetEffect();
}
