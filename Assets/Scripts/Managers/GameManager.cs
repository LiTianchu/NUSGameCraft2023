using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static readonly int YEAR_STEP = 1;
    private static readonly float YEAR_ADVANCE_RATE = 1f;
    private static readonly float DESTRUCTION_STEP = 0.01f;
    private static readonly float DESTRUCTION_ADVANCE_RATE = 0.1f;
    private static readonly float DESTRUCTION_ACCELRATION_STEP = 0.01f;
    private static readonly float DESTRUCTION_ACCELERATION_RATE = 10f;
    private static readonly float MAX_DESTRUCTION_STEP = 1f;

    private bool _isGameOver = false;
    private bool _isGamePaused = false;
    private int _currentYear = 2023;
    private float _destructionGauge = 0;

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
    }
    // Update is called once per frame
    void Update()
    {
        _timePassed += _isGamePaused ? 0 : Time.deltaTime;

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

            _nextDestructionAccelerationTime = _timePassed + DESTRUCTION_ACCELERATION_RATE;
            _finalDestructionAdvanceStep += DESTRUCTION_ACCELRATION_STEP;
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
    }

    public void AddDestruction(float destruction)
    {
        Debug.Log("Excalibur");
        _destructionGauge += destruction;
    }

    public void GameOver()
    {
        _isGameOver = true;
        PauseGame();
        Debug.Log("GameOver");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isGamePaused = false;
    }

}
