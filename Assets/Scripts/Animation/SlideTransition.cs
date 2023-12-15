using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SlideTransition : Singleton<SlideTransition>
{
    [SerializeField]
    private SlideDirection onStartSlideMode;

    private RectTransform _widgetUIRect;
    private CanvasGroup _widgetCanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        _widgetCanvasGroup = GetComponent<CanvasGroup>();
        _widgetUIRect = GetComponent<RectTransform>();

        switch (onStartSlideMode)
        {
            case SlideDirection.CenterToRight:
                SlideToRightFromCenter();
                break;
            case SlideDirection.CenterToLeft:
                SlideToLeftFromCenter();
                break;
            case SlideDirection.LeftToCenter:
                SlideToCenterFromLeft();
                break;
            default:
                break;
        }   
    }

    public void SlideToLeft()
    {
        UIManager.Instance.WidgetSlide(_widgetCanvasGroup, 0.5f, new Vector3(0, 0, 0), new Vector3(-_widgetUIRect.rect.width, 0, 0));
    }

    public void SlideToRight()
    {
        UIManager.Instance.WidgetSlide(_widgetCanvasGroup, 0.5f, new Vector3(0, 0, 0), new Vector3(_widgetUIRect.rect.width, 0, 0));
    }

    public void SlideToCenterFromLeft()
    {
        UIManager.Instance.WidgetSlide(_widgetCanvasGroup, 0.5f, new Vector3(-_widgetUIRect.rect.width, 0, 0), new Vector3(0, 0, 0));
    }

    public void SlideToRightFromCenter()
    {
        UIManager.Instance.WidgetSlide(_widgetCanvasGroup, 0.5f, new Vector3(0, 0, 0), new Vector3(_widgetUIRect.rect.width, 0, 0));
    }

    public void SlideToLeftFromCenter()
    {
        UIManager.Instance.WidgetSlide(_widgetCanvasGroup, 0.5f, new Vector3(0, 0, 0), new Vector3(-_widgetUIRect.rect.width, 0, 0));
    }

    private enum SlideDirection
    {
        CenterToRight,
        CenterToLeft,
        LeftToCenter,
    }
}
