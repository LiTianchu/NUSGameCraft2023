using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
