using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryBook : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField]
    private string[] sentences;
    [SerializeField]
    private TMP_Text textDisplay;
    [SerializeField]
    private float typingRate;
    [SerializeField]
    private float sentencePauseTime;
    [SerializeField]
    private CanvasGroup options;
    [SerializeField]
    private CanvasGroup footer;
    [SerializeField]
    private AudioClip diaryBGM;

    private int _currentSentenceIndex = 0;
    //private int _currentSentenceCharIndex;
    private bool _nextSentenceReady = true;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SetBGMPlayRate(1);
        AudioManager.Instance.PlayBGM(diaryBGM);
    }

    // Update is called once per frame
    void Update()
    {
        //string currentSentence = sentences[_currentSentenceIndex];
        if (_nextSentenceReady)
        {
            if(_currentSentenceIndex >= sentences.Length)
            {
                if (!footer.gameObject.activeSelf)
                {
                    footer.gameObject.SetActive(true);
                    UIManager.Instance.WidgetFadeIn(footer, 0.5f, footer.GetComponent<RectTransform>().anchoredPosition, footer.GetComponent<RectTransform>().anchoredPosition);
                    StartCoroutine(GoToGame());
                }
                return;
            }
            StartCoroutine(TypeSentence(sentences[_currentSentenceIndex]));
            _nextSentenceReady = false;
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        if (sentence.Equals("<difficultySelection>"))
        {
            PromptForOption();
        }
        else
        {

            foreach (char letter in sentence.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingRate);
            }

            yield return new WaitForSeconds(sentencePauseTime);
            _nextSentenceReady = true;
            _currentSentenceIndex++;
        }
    }

    public void PromptForOption()
    {
        UIManager.Instance.WidgetFadeIn(options, 0.5f, new Vector2(0, -100), new Vector2(0, 30));
    }

    public void SelectOption(string optionContent)
    {
        _nextSentenceReady = true;
        sentences[_currentSentenceIndex] = optionContent;
        UIManager.Instance.WidgetFadeIn(options, 0.5f, new Vector2(0, 30), new Vector2(0, -100));
    }

    public void SetDifficulty(int difficulty)
    {
        GameManager.Instance.Difficulty = difficulty;
    }

    IEnumerator GoToGame()
    {
        yield return new WaitForSeconds(2.5f);
        SlideTransition.Instance.SlideToCenterFromLeft();
        StartCoroutine(GameManager.Instance.LoadScene("MainScene", 0.5f));
    }

    
}
