using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverHint : Singleton<HoverHint>
{
    [SerializeField]
    private TMP_Text hintContent;
    [SerializeField]
    private float maxWidth;

    private float width;
    private float height;

    private RectTransform _rectTransform;

    public float Width { get => width; set => width = value; }
    public float Height { get => height; set => height = value; }

    private void Start()
    {
        gameObject.SetActive(false);
        _rectTransform = GetComponent<RectTransform>();
        Debug.Log(Screen.width.ToString() + " " + Screen.height.ToString());
    }

    public void ShowHint(string content, Vector2 screenPos, bool autoSize)
    {

        float width = 0;
        float height = 0;
        hintContent.ForceMeshUpdate();
        if (autoSize)
        {
            width = hintContent.preferredWidth < maxWidth ? hintContent.preferredWidth : maxWidth;
            height = hintContent.preferredHeight;
        }
        else
        {
            width = Width;
            height = Height;
        }

        _rectTransform.sizeDelta = new Vector2(width, height);
        Debug.Log(width.ToString() + " " + height.ToString());

        //prevent it from going out of screen
        if (screenPos.x + width > Screen.width)
        {
            screenPos.x = Screen.width - width;
        }else if(screenPos.x - width < 0)
        {
            screenPos.x = width;
        }

        if(screenPos.y + height > Screen.height)
        {
            screenPos.y = Screen.height - height;
        }else if(screenPos.y - height < 0)
        {
            screenPos.y = height;
        }
        
        //set active and show content
        gameObject.SetActive(true);
        transform.position = screenPos;
        hintContent.text = content;
    }

    //public void ShowHintOnCanvasPos(string content, Vector2 canvasPos)
    //{
    //    hintContent.ForceMeshUpdate();
    //    float width = hintContent.preferredWidth < maxWidth ? hintContent.preferredWidth : maxWidth;
    //    float height = hintContent.preferredHeight;
    //    _rectTransform = GetComponent<RectTransform>();
    //    _rectTransform.sizeDelta = new Vector2(width, height);

    //    //prevent it from going out of screen
    //    if (canvasPos.x + width > Screen.width)
    //    {
    //        canvasPos.x = Screen.width - width;
    //    }
    //    else if (canvasPos.x - width < 0)
    //    {
    //        canvasPos.x = width;
    //    }

    //    if (canvasPos.y + height > Screen.height)
    //    {
    //        canvasPos.y = Screen.height - height;
    //    }
    //    else if (canvasPos.y - height < 0)
    //    {
    //        canvasPos.y = height;
    //    }

    //    //set active and show content
    //    gameObject.SetActive(true);
    //    transform.position = canvasPos;
    //    hintContent.text = content;
    //}

    public void HideHint()
    {
        gameObject.SetActive(false);
    }
}
