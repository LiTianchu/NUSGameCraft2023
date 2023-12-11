using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    private ResourceType resourceType;
    [SerializeField]
    private TMP_Text qtyField;

    private int _qty;

    private void FixedUpdate()
    {
        UpdateQty();
    }

    private void UpdateQty()
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                _qty = ResourceManager.Instance.WoodQty;
                break;
            case ResourceType.Rock:
                _qty = ResourceManager.Instance.RockQty;
                break;
            case ResourceType.Crystal:
                _qty = ResourceManager.Instance.CystalQty;
                break;
            case ResourceType.Water:
                _qty = ResourceManager.Instance.WaterQty;
                break;
        }

        qtyField.text = _qty.ToString();
    }




}
