using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private int _woodQty;
    private int _rockQty;
    private int _crystalQty;
    private int _waterQty;
    private int _rebellionQty;

    public int WoodQty {  get { return _woodQty; } }
    public int RockQty { get { return _rockQty; } }
    public int CystalQty { get { return _crystalQty; } }
    public int WaterQty { get { return _waterQty; } }
    public int RebellionQty { get { return _rebellionQty; } set { _rebellionQty = value; } }
    
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

    public void AddRebellion(int rebellionQty)
    {
        _rebellionQty += rebellionQty;
    }

    public bool RemoveWood(int woodQty)
    {
        if (_woodQty < woodQty) {
            return false;

        }
        _woodQty -= woodQty;
        return true;
    }

    public bool RemoveRock(int rockQty)
    {
        if (_rockQty < rockQty)
        {
            return false;

        }
        _rockQty -= rockQty;
        return true;
    }

    public bool RemoveCrystal(int crystalQty)
    {
        if (_crystalQty < crystalQty)
        {
            return false;

        }
        _crystalQty -= crystalQty;
        return true;
    }

    public bool RemoveWater(int waterQty)
    {
        if (_waterQty < waterQty)
        {
            return false;

        }
        _waterQty -= waterQty;
        return true;
    }

    public bool RemoveRebellion(int rebellionQty)
    {
        if (_rebellionQty < rebellionQty)
        {
            return false;

        }
        _rebellionQty -= rebellionQty;
        return true;
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
            case ResourceType.Rebellion:
                AddRebellion(resourceQty); break;
        }
    }

    public bool RemoveResource(int resourceQty, ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                return RemoveWood(resourceQty);
            case ResourceType.Rock:
                return RemoveRock(resourceQty);
            case ResourceType.Crystal:
                return RemoveCrystal(resourceQty);
            case ResourceType.Water:
                return RemoveWater(resourceQty);
            case ResourceType.Rebellion:
                return RemoveRebellion(resourceQty);  
        }

        return false;
    }

    public void AddResource(int wood, int rock, int crystal, int water)
    {
        AddWood(wood);
        AddRock(rock);
        AddCrystal(crystal);
        AddWater(water);
    }

    public bool RemoveResource(int wood, int rock, int crystal, int water)
    {
        int originalWood = _woodQty; //store original qty
        int originalRock = _rockQty;
        int originalCrystal = _crystalQty;
        int originalWater = _waterQty;

        bool allSuccess = RemoveWood(wood) && RemoveRock(rock) && RemoveCrystal(crystal) && RemoveWater(water);
        if (!allSuccess) //if not all success, restore original qty
        {
            _woodQty = originalWood;
            _rockQty = originalRock;
            _crystalQty = originalCrystal;
            _waterQty = originalWater;
            WarningToast.Instance.ShowToast("Not enough resources!");
        }
        return allSuccess;
    }

}
