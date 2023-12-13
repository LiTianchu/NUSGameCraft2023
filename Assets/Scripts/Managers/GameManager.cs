using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static readonly int YEAR_STEP = 1;
    private static readonly float DESTRUCTION_STEP = 0.1f;
    private static readonly float YEAR_ADVANCE_RATE = 1f;
    private static readonly float DESTRUCTION_ADVANCE_RATE = 0.1f;

    private bool _isGameOver = false;
    private int _currentYear = 2023;
    private float _destructionGauge = 0;
    private float _yearAdvanceTimer = 0f;
    private float _destructionAdvanceTimer = 0f;
    //private int _totalRebellion = 0;

    public int CurrentYear { get { return _currentYear; }}
    public float DestructionGauge { get { return _destructionGauge; }}    
    public bool IsGameOver { get { return _isGameOver; }} 
    //public int TotalRebellion { get { return _totalRebellion; } set { _totalRebellion = value; } }

    // Update is called once per frame
    void Update()
    {
        _yearAdvanceTimer += Time.deltaTime;
        _destructionAdvanceTimer += Time.deltaTime;
        if (_yearAdvanceTimer >= YEAR_ADVANCE_RATE && !_isGameOver)
        {
            _yearAdvanceTimer = 0f;
            AdvanceYear();

            
        }

        if (_destructionAdvanceTimer >= DESTRUCTION_ADVANCE_RATE && !_isGameOver)
        {
            _destructionAdvanceTimer = 0f;
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
    }

    public void AdvanceYear()
    {
        _currentYear += YEAR_STEP;
    }

    public void AdvanceDestruction()
    {
        _destructionGauge += DESTRUCTION_STEP - (float)ResourceManager.Instance.RebellionQty/10000;
    }

    public void GameOver()
    {
        _isGameOver = true;
        Debug.Log("GameOver");
    }
    
}
