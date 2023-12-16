using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulatorManager : Singleton<SimulatorManager>
{
    [SerializeField]
    private CanvasGroup winPanel;
    [SerializeField]
    private CanvasGroup losePanel;
    [SerializeField]
    private CanvasGroup optionsPanel;
    [SerializeField]
    private CanvasGroup helpPanel;

    [SerializeField]
    private AudioClip simulatorBGM;
    [SerializeField]
    private AudioClip winBGM;
    [SerializeField]
    private AudioClip loseBGM;

    private static readonly int YEAR_STEP = 1;
    private static readonly float YEAR_ADVANCE_RATE = 1f;
    private static readonly float DESTRUCTION_STEP = 0.01f;
    private static readonly float DESTRUCTION_ADVANCE_RATE = 0.1f;
    private static readonly float MAX_DESTRUCTION_STEP = 1f;

    private float _destructionAccelerationStep = 0.01f;
    private float _destructionAccelerationRate = 8f;

    private bool _isGameOver = false;
    private bool _isGamePaused = false;
    private int _currentYear = 2023;
    private float _destructionGauge = 10;

    private float _timePassed = 0f;
    private float _nextYearTime = 0f;
    private float _nextDestructionTime = 0f;
    private float _nextDestructionAccelerationTime = 0f;
    private float _finalDestructionAdvanceStep = DESTRUCTION_STEP;

    public int CurrentYear { get { return _currentYear; } }
    public float DestructionGauge { get { return _destructionGauge; } }
    public bool IsGameOver { get { return _isGameOver; } }
    public float TimePassed { get { return _timePassed; } }

    private void Start()
    {
        //Time.timeScale = 10;
        //GameManager.Instance.Difficulty = 2;
        ToggleHelp();
        AudioManager.Instance.SetBGMPlayRate(1);
        AudioManager.Instance.PlayBGM(simulatorBGM);
        Debug.Log("Difficulty: " + GameManager.Instance.Difficulty);
        _destructionAccelerationStep = _destructionAccelerationStep + _destructionAccelerationStep * ((float)GameManager.Instance.Difficulty/8);
        Debug.Log("Dest Acce Step: " + _destructionAccelerationStep);
        _destructionAccelerationRate = _destructionAccelerationRate * (1- (float)GameManager.Instance.Difficulty/8);
        Debug.Log("Dest Acce Rate: " + _destructionAccelerationRate);

        _destructionGauge = GameManager.Instance.Difficulty*_destructionGauge + _destructionGauge;
    }
    // Update is called once per frame
    void Update()
    {
        _timePassed += _isGamePaused || IsPanelOpen() ? 0 : Time.deltaTime;

        if (_timePassed >= _nextYearTime && !_isGameOver)
        {
            _nextYearTime = _timePassed + YEAR_ADVANCE_RATE;
            AdvanceYear();


        }

        if (_timePassed >= _nextDestructionTime && !_isGameOver)
        {
            _nextDestructionTime = _timePassed + DESTRUCTION_ADVANCE_RATE;
            if (_destructionGauge >= 100)
            {
                _destructionGauge = 100;
                GameOver();
            }
            else
            {
                AdvanceDestruction();
            }
        }

        if (_timePassed >= _nextDestructionAccelerationTime && !_isGameOver)
        {

            _nextDestructionAccelerationTime = _timePassed + _destructionAccelerationRate;
            _finalDestructionAdvanceStep += _destructionAccelerationStep;
            if (_finalDestructionAdvanceStep > MAX_DESTRUCTION_STEP)
            {

                _finalDestructionAdvanceStep = MAX_DESTRUCTION_STEP;
            }
        }
    }

    public void AdvanceYear()
    {
        _currentYear += YEAR_STEP;
    }

    public void AdvanceDestruction()
    {
        _destructionGauge += _finalDestructionAdvanceStep - (float)ResourceManager.Instance.RebellionQty / 1000;
        if(_destructionGauge <= 0)
        {
            WinGame();
        }
    }

    public void AddDestruction(float destruction)
    {
        Debug.Log("Excalibur");
        _destructionGauge += destruction;
    }

    public void GameOver()
    {
        _isGameOver = true;
        _isGamePaused = true;
        losePanel.gameObject.SetActive(true);
        AudioManager.Instance.SetBGMPlayRate(1f);
        AudioManager.Instance.PlayBGM(loseBGM);
        UIManager.Instance.WidgetFadeIn(losePanel, 0.5f, new Vector2(0, 0), new Vector2(0, 0));
        Debug.Log("GameOver");
    }

    public void WinGame()
    {
        _isGamePaused = true;
        winPanel.gameObject.SetActive(true);
        AudioManager.Instance.SetBGMPlayRate(1f);
        AudioManager.Instance.PlayBGM(winBGM);
        UIManager.Instance.WidgetFadeIn(winPanel, 0.5f, new Vector2(0, 0), new Vector2(0, 0));
        Debug.Log("WinGame");
    }

    public void PauseGame()
    {
        //Time.timeScale = 0;
        _isGamePaused = true;
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1;
        _isGamePaused = false;
    }

    public void RestartScene()
    {
        SlideTransition.Instance.SlideToCenterFromLeft();
        StartCoroutine(GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 0.5f));
    }

    public void BackToMenu()
    {
        SlideTransition.Instance.SlideToCenterFromLeft();
        StartCoroutine(GameManager.Instance.LoadScene("Home", 0.5f));
    }

    public void QuitGame()
    {
        SlideTransition.Instance.SlideToCenterFromLeft();
        StartCoroutine(GameManager.Instance.QuitGame(0.5f));
    }

    public void ToggleOptions()
    {
        if (optionsPanel.gameObject.activeSelf)
        {
            optionsPanel.gameObject.SetActive(false);
        }
        else
        {
            optionsPanel.gameObject.SetActive(true);
        }
    }

    public void ToggleHelp()
    {
        if (helpPanel.gameObject.activeSelf)
        {
            helpPanel.gameObject.SetActive(false);
        }
        else
        {
            helpPanel.gameObject.SetActive(true);
        }
    }

    private bool IsPanelOpen()
    {
        return optionsPanel.gameObject.activeSelf || helpPanel.gameObject.activeSelf;
    }

}
