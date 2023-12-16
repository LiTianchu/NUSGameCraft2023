using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField]
    private AudioClip homeBGM;
    [SerializeField]
    private CanvasGroup creditsPanel;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        AudioManager.Instance.SetBGMPlayRate(1);
        AudioManager.Instance.PlayBGM(homeBGM);
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        StartCoroutine(GameManager.Instance.LoadScene("Prologue", 0.5f));
        SlideTransition.Instance.SlideToCenterFromLeft();
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        StartCoroutine(GameManager.Instance.QuitGame(0.5f));
        SlideTransition.Instance.SlideToCenterFromLeft();

        
    }

    public void ToggleCredits()
    {
        if (!creditsPanel.gameObject.activeSelf) //show credits
        {
            creditsPanel.gameObject.SetActive(true);
            UIManager.Instance.WidgetFadeIn(creditsPanel, 0.5f, new Vector2(0, 0), new Vector2(0, 0));
        }
        else //hide credits
        {
            creditsPanel.gameObject.SetActive(false);
            UIManager.Instance.WidgetFadeOut(creditsPanel, 0.5f, new Vector2(0, 0), new Vector2(0, 0));
        }
    }
    
}
