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
    private RectTransform _rectTransform;
    private void Start()
    {
        gameObject.SetActive(false);
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ShowHint(string content, Vector2 position)
    {
        //resize the hint box to fit the content
        hintContent.ForceMeshUpdate();
        float width = hintContent.preferredWidth < maxWidth? hintContent.preferredWidth : maxWidth;
        float height = hintContent.preferredHeight;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(width, height);

        //prevent it from going out of screen
        if(position.x + width/2 > Screen.width)
        {
            position.x = Screen.width - width/2;
        }else if(position.x - width / 2 < 0)
        {
            position.x = width / 2;
        }

        if(position.y + height/2 > Screen.height)
        {
            position.y = Screen.height - height/2;
        }else if(position.y - height/2 < 0)
        {
            position.y = height/2;
        }
        
        //set active and show content
        gameObject.SetActive(true);
        transform.position = position;
        hintContent.text = content;
    }

    public void HideHint()
    {
        gameObject.SetActive(false);
    }
}
