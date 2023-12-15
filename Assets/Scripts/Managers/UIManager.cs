using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GlobalSingleton<UIManager>
{

    public void WidgetFadeIn(CanvasGroup widgetCanvasGroup, float fadeTime, Vector3 transitionFromAnchorPos, Vector3 transitionTargetAnchorPos)
    {
        RectTransform widgetUIRect = widgetCanvasGroup.gameObject.GetComponent<RectTransform>();
        widgetCanvasGroup.alpha = 0f; //starting opacity
        widgetUIRect.anchoredPosition = transitionFromAnchorPos; //starting position stays
        widgetUIRect.DOAnchorPos(transitionTargetAnchorPos, fadeTime, false).SetEase(Ease.OutFlash); //falsh in animation
        widgetCanvasGroup.DOFade(1, fadeTime); //fade out animtion
    }

    public void WidgetFadeOut(CanvasGroup widgetCanvasGroup, float fadeTime, Vector3 transitionFromAnchorPos, Vector3 transitionTargetAnchorPos)
    {
        RectTransform widgetUIRect = widgetCanvasGroup.gameObject.GetComponent<RectTransform>();
        widgetCanvasGroup.alpha = 1f; //starting opacity
        widgetUIRect.anchoredPosition = transitionFromAnchorPos;
        widgetUIRect.DOAnchorPos(transitionTargetAnchorPos, fadeTime, false).SetEase(Ease.OutFlash); //falsh in animation
        widgetCanvasGroup.DOFade(0, fadeTime); //fade out animtion
    }

    public void WidgetSlide(CanvasGroup widgetCanvasGroup, float slideTime, Vector3 transitionFromAnchorPos, Vector3 transitionTargetAnchorPos)
    {
        RectTransform widgetUIRect = widgetCanvasGroup.gameObject.GetComponent<RectTransform>();
        widgetUIRect.anchoredPosition = transitionFromAnchorPos;
        widgetUIRect.DOAnchorPos(transitionTargetAnchorPos, slideTime, false).SetEase(Ease.OutFlash); //falsh in animation
    }

    
}
