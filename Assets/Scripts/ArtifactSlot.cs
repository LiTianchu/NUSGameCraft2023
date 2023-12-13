using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlot : MonoBehaviour
{
    [SerializeField]
    private Image artifactImg;

    public IArtifact Artifact { get; set; }
    
}
