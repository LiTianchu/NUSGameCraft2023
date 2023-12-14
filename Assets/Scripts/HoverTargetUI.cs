using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTargetUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(5, 10)]
    [SerializeField]
    private string hintContent;
    [SerializeField]
    private RectTransform hintSpawnAnchor;
    [SerializeField]
    private float height;
    [SerializeField]
    private float width;

    //private void Start()
    //{
        
    //    HoverHint.Instance.Width = width;
    //    HoverHint.Instance.Height = height;
    //    Debug.Log("Set width and height");
        
    //}
    public void SetHintContent(string content)
    {
        hintContent = content;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverHint.Instance.Width = width;
        HoverHint.Instance.Height = height;
        if (hintSpawnAnchor == null)
        {
            HoverHint.Instance.ShowHint(hintContent, this.transform.position,false);
        }
        else
        {
            HoverHint.Instance.ShowHint(hintContent, hintSpawnAnchor.transform.position, false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverHint.Instance.HideHint();
    }
}
