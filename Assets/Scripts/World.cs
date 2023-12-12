using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class World : MonoBehaviour
{

    [SerializeField]
    private TMP_Text adventurerNumDisplay;
    

    protected float _secPassed;
    protected HashSet<Adventurer> _adventurers;

    public virtual void Start()
    {
        _adventurers = new HashSet<Adventurer>();
    }

    public virtual void Update()
    {
        _secPassed += Time.deltaTime;

        
        adventurerNumDisplay.text = _adventurers.Count.ToString();
    }

    public void AddAdventurer(Adventurer adv)
    {
        adv.transform.SetParent(transform);
        _adventurers.Add(adv);
        adv.OnAdventurerDead += RemoveAdventurer;
    }

    private void RemoveAdventurer(Adventurer adv)
    {
        _adventurers.Remove(adv);
        adv.OnAdventurerDead -= RemoveAdventurer;
        Destroy(adv.gameObject);
    }
}
