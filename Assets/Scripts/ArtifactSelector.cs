using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    // Start is called before the first frame update

    private ArtifactSlot _selectedSlot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleArtifactPanel(ArtifactSlot slot)
    {
        
        panel.transform.localPosition = slot.transform.localPosition + new Vector3(0, 100, 0);
        if(_selectedSlot == slot)
        {
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            panel.SetActive(true);
        }
            
        _selectedSlot = slot;
    }

    public void SetArtifact(ArtifactDecorator artifact)
    {
        _selectedSlot.Artifact = artifact;
    }
}
