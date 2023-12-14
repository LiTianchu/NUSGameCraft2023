using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;

    private void Start()
    {
        pause.onClick.AddListener(Pause);
        resume.onClick.AddListener(Resume);
    }

    private void Pause()
    {
        GameManager.Instance.PauseGame();
        resume.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }

    private void Resume()
    {
        GameManager.Instance.ResumeGame();
        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
    }

}
