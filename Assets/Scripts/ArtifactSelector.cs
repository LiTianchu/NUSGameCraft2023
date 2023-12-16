using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSelector : Singleton<ArtifactSelector>
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private List<ArtifactSlot> artifactSlots;
    [SerializeField]
    private AudioClip setArtifactSound;
    [SerializeField]
    private AudioClip removeArtifactSound;
    [SerializeField]
    private AudioClip openArtifactSelectionSound;
    // Start is called before the first frame update

    private ArtifactSlot _selectedSlot;
    public List<ArtifactSlot> ArtifactSlots { get => artifactSlots; set => artifactSlots = value; }
    public void ToggleArtifactPanel(ArtifactSlot slot)
    {

        panel.transform.localPosition = slot.transform.localPosition + new Vector3(0, 100, 0);
        if (_selectedSlot == slot)
        {
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            panel.SetActive(true);
        }

        _selectedSlot = slot;
    }

    public void SetArtifact(Artifact artifact)
    {
        if(_selectedSlot.Artifact !=null)
        {
            Debug.Log("Artifact not null");
            RemoveArtrifact();
            SetArtifact(artifact);
            return;
        }

        if (artifact != null &&
            ResourceManager.Instance.RemoveResource(artifact.WoodCost,
                                                        artifact.RockCost,
                                                            artifact.CrystalCost,
                                                                artifact.WaterCost))
        {
            _selectedSlot.Artifact = artifact;
            panel.SetActive(false);
            AudioManager.Instance.PlaySFX(setArtifactSound);
        }
        
    }

    public void RemoveArtrifact()
    {
        if (_selectedSlot.Artifact != null)
        {
            ResourceManager.Instance.AddResource(_selectedSlot.Artifact.WoodCost,
                                                    _selectedSlot.Artifact.RockCost,
                                                        _selectedSlot.Artifact.CrystalCost,
                                                            _selectedSlot.Artifact.WaterCost);
        }
        _selectedSlot.Artifact = null;
        panel.SetActive(false);
        AudioManager.Instance.PlaySFX(removeArtifactSound);
    }

    public void ClearAllSlots()
    {
        foreach (ArtifactSlot slot in artifactSlots)
        {
            slot.Artifact = null;
        }
    }
}
