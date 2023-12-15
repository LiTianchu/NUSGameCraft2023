using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        _showHintTime = SimulatorManager.Instance.TimePassed + waitTime;
        //HoverHint.Instance.ShowHint(hintContent, Input.mousePosition);
        
    }

    private void OnMouseExit()
    {
        HoverHint.Instance.HideHint();

    }

    private void OnMouseOver()
    {
        if(SimulatorManager.Instance.TimePassed >= _showHintTime)
        {
            //Debug.Log(Input.mousePosition);
            HoverHint.Instance.ShowHint(hintContent, Input.mousePosition,true);
        }
    }



    public void SetHintContent(string content)
    {
        hintContent = content;
    }

    
}
