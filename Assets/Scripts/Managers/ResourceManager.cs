using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private int _woodQty;
    private int _rockQty;
    private int _crystalQty;
    private int _waterQty;

    public int WoodQty {  get { return _woodQty; } }
    public int RockQty { get { return _rockQty; } }
    public int CystalQty { get { return _crystalQty; } }
    public int WaterQty { get { return _waterQty; } }

    public void AddWood(int woodQty)
    {
        _woodQty += woodQty;
    }

    public void AddRock(int rockQty)
    {
        _rockQty += rockQty;
    }

    public void AddCrystal(int crystalQty)
    {
        _crystalQty += crystalQty;
    }

    public void AddWater(int waterQty)
    {
        _waterQty += waterQty;
    }

    public void RemoveWood(int woodQty)
    {
        _woodQty -= woodQty;
    }

    public void RemoveRock(int rockQty)
    {
        _rockQty -= rockQty;
    }

    public void RemoveCrystal(int crystalQty)
    {
        _crystalQty -= crystalQty;
    }

    public void RemoveWater(int waterQty)
    {
        _waterQty -= waterQty;
    }

    public void AddResource(int resourceQty, ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                AddWood(resourceQty); break;
            case ResourceType.Rock:
                AddRock(resourceQty); break;
            case ResourceType.Crystal:
                AddCrystal(resourceQty); break;
            case ResourceType.Water:
                AddWater(resourceQty); break;
        }
    }

    public void RemoveResource(int resourceQty, ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                RemoveWood(resourceQty); break;
            case ResourceType.Rock:
                RemoveRock(resourceQty); break;
            case ResourceType.Crystal:
                RemoveCrystal(resourceQty); break;
            case ResourceType.Water:
                RemoveWater(resourceQty); break;
        }
    }

}
