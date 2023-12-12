using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFactory : Factory
{
    [SerializeField]
    private Soul soulPrefab;
    private Dictionary<string, bool> _maleSoulNameDict; //bool is true if name is taken
    private Dictionary<string, bool> _femaleSoulNameDict; //bool is true if name is taken

    void Start()
    {
        InitializeNameGeneration();
    }

    private void InitializeNameGeneration() {
        _maleSoulNameDict = new Dictionary<string, bool>();
        _femaleSoulNameDict = new Dictionary<string, bool>();

        foreach (string name in NamePresets.maleNames)
        {
            _maleSoulNameDict.Add(name, false);
        }

        foreach (string name in NamePresets.femaleNames)
        {
            _femaleSoulNameDict.Add(name, false);
        }
    }

    public IProduct GetProduct(GenderType gender)
    {
        Soul soul = Instantiate(soulPrefab);
        string name = GenerateName(gender);
        soul.Gender = gender;
        soul.SoulName = name;
        soul.gameObject.name = name;
        return soul;
    }

    public IProduct GetProduct()
    {
        return GetProduct((GenderType)Random.Range(0, 2));
    }

    private string GenerateName(GenderType genderType)
    {
        if(_femaleSoulNameDict == null || _maleSoulNameDict == null)
        {
            InitializeNameGeneration();
        }

        string name = "";
        string[] nameArr = genderType == GenderType.Female ? NamePresets.femaleNames : NamePresets.maleNames;
        Dictionary<string, bool> dict = genderType == GenderType.Female ? _femaleSoulNameDict : _maleSoulNameDict;
        do //generate a name that is not taken
        {
            name = nameArr[UnityEngine.Random.Range(0, nameArr.Length)];
        } while (dict[name]); //loop while name is taken

        dict[name] = true;
        return name;
    }

}
