using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WarningToast : Singleton<WarningToast>
{
    [SerializeField]
    private float showHeight;
    [SerializeField]
    private float hideHeight;
    [SerializeField]
    private float showDuration;
    [SerializeField]
    private AudioClip warningSound;

    private TMP_Text _text;
    private RectTransform _rectTransform;
    private float _timeToHide;
    private CanvasGroup _canvasGroup;
    private bool _isShowing;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TMP_Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform.anchoredPosition = new Vector2(0, hideHeight);
    }

    private void Update()
    {
        if (SimulatorManager.Instance.TimePassed >= _timeToHide && _isShowing)
        {
            HideToast();
        }
    }

    public void ShowToast(string content)
    {
        UIManager.Instance.WidgetFadeIn(_canvasGroup, 0.5f, new Vector2(0, hideHeight), new Vector2(0,showHeight));
        _timeToHide = SimulatorManager.Instance.TimePassed + showDuration;
        _text.text = content;
        _isShowing = true;
        AudioManager.Instance.PlaySFX(warningSound);
    }

    public void HideToast()
    {
        UIManager.Instance.WidgetFadeOut(_canvasGroup, 0.5f, new Vector2(0, showHeight), new Vector2(0, hideHeight));
        _isShowing = false;
    }
}
