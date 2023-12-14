using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HoverTarget : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField]
    private string hintContent;
    [SerializeField]
    private float waitTime;

    private float _showHintTime;
    private void OnMouseEnter()
    {
        _showHintTime = GameManager.Instance.TimePassed + waitTime;
        
    }

    private void OnMouseExit()
    {
        HoverHint.Instance.HideHint();

    }

    private void OnMouseOver()
    {
        if(GameManager.Instance.TimePassed > _showHintTime)
        {
            HoverHint.Instance.ShowHint(hintContent, Input.mousePosition);
        }
    }
}
