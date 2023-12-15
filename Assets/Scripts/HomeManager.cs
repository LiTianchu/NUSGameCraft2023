using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : Singleton<HomeManager>
{
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
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
}
