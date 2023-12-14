using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlot : MonoBehaviour
{
    [SerializeField]
    private Image artifactImg;
    [SerializeField]
    private GameObject placeHolder;

    public Artifact Artifact { get; set; }


    public void Update()
    {
        if (Artifact != null)
        {
            artifactImg.sprite = Artifact.ArtifactSprite;
            placeHolder.SetActive(false);
            artifactImg.gameObject.SetActive(true);
            artifactImg.SetNativeSize();
        }
        else
        {
            artifactImg.gameObject.SetActive(false);
            placeHolder.SetActive(true);
        }
    }
}
