using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddCapacity : MonoBehaviour
{
    [SerializeField]
    private TMP_Text woodCostDisplay;
    [SerializeField]
    private TMP_Text rockCostDisplay;
    //[SerializeField]
    //private TMP_Text capacityDisplay;

    [SerializeField]
    private int woodCost;
    [SerializeField]
    private int rockCost;
    [SerializeField]
    private int costStepEachTime;

    public int WoodCost { get => woodCost; set => woodCost = value; }
    public int RockCost { get => rockCost; set => rockCost = value; }
    // Update is called once per frame
    void Update()
    {
        woodCostDisplay.text = woodCost.ToString();
        rockCostDisplay.text = rockCost.ToString();
    }

    public void AddStoneCost()
    {
        rockCost += costStepEachTime;
    }

    public void AddWoodCost()
    {
        woodCost += costStepEachTime;
    }
}
