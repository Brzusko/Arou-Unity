using System;
using EventArguments;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    #region Properties
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            return null;
        }
    }

    public bool IsDebugModeOn
    {
        get
        {
            return _isDebugModeOn;
        }
    }

    public float MaxForce
    {
        get { return _maxForce; }
    }

    public float ForceMultipler
    {
        get { return _forceMultipler; }
    }

    public int Score
    {
        get { return _playerScore; }
    }

    public int PipeSpawnCount
    {
        get
        {
            if(Score == 0)
                return 3;

            var toReturn = 3 + Mathf.FloorToInt(Score * 0.05f);
            return toReturn;
        }
    }

    public float MinPadWith
    {
        get => _minWidthBetweenPads;
    }

    public float MaxPadWith
    {
        get => _maxWidthBetweenPads;
    }

    public float MinPadHeight
    {
        get => _minHeightBetweenPads;
    }

    public float MaxPadHeight
    {
        get => _maxHeightBetweenPads;
    }

    public float Gravity
    {
        get => _playerGravity;
    }

    public PipeSpawner PipeSpawner { get => _pipeSpawner; }
    #endregion

    #region Private settings and vars

    [Header("Main Settings")]
    [SerializeField]
    private int _playerScore = 0;
    [SerializeField]
    private float _maxWidthBetweenPads = 0.0f;
    [SerializeField]
    private float _minWidthBetweenPads = 0.0f;
    [SerializeField]
    private float _maxHeightBetweenPads = 0.0f;
    [SerializeField]
    private float _minHeightBetweenPads = 0.0f;
    [SerializeField]
    private float _forceMultipler = 0.0f;
    [SerializeField]
    private float _maxForce = 0.0f;
    [SerializeField]
    private bool _isDebugModeOn = true;
    [SerializeField]
    private float _widthLimit = 0.0f;
    [SerializeField]
    private float _heightLimit = 0.0f;
    [SerializeField]
    private float _playerGravity = 0.0f;
    
    [Header("Systems")]
    [SerializeField]
    private PipeSpawner _pipeSpawner;
    [SerializeField]
    private Text playerScore;
    [SerializeField]
    private GameObject panel;
    #endregion

    #region Events
    public event EventHandler<OnShootArgs> OnShootEvent;
    public event EventHandler<OnFaileArgs> OnFailEvent;
    public event EventHandler<OnScoreArgs> OnScoreEvent;
    #endregion

    protected virtual void OnShootEventHandler(OnShootArgs onShootArgs)
    {
        OnShootEvent?.Invoke(this, onShootArgs);
    }

    protected virtual void OnFailEventHandler(OnFaileArgs onFaileArgs)
    {
        panel.SetActive(true);
        playerScore.color = Color.black;
        OnFailEvent?.Invoke(this, onFaileArgs);
    }

    protected virtual void OnScoreEventHandler(OnScoreArgs onScoreArgs)
    {
        _playerScore++;
        RecalculateSettings();
        var score = new OnScoreArgs { Score = _playerScore };
        StatsManager.Instance.playerScore = score.Score;
        OnScoreEvent?.Invoke(this, score);
    }

    private void CaluclateMinWidth()
    {
        var newValue = (_playerScore * 0.03f - 3f) * (-1f);
        newValue = Mathf.Clamp(newValue, _widthLimit, 5.0f);
        _minWidthBetweenPads = newValue;
    }

    private void CalculateMaxWidth()
    {
        _maxWidthBetweenPads = _minWidthBetweenPads + 0.8f;
    }

    private void CalculateMinHeight()
    {
        var newValue = (_playerScore * 0.05f - 3f) * (-1f);
        newValue = Mathf.Clamp(newValue, _heightLimit, 5.0f);
        _minHeightBetweenPads = newValue;
    }

    private void CalculateMaxHeight()
    {
        _maxHeightBetweenPads = _minHeightBetweenPads + 0.2f;
    }
    private void RecalculateSettings()
    {
        CaluclateMinWidth();
        CalculateMaxWidth();
        CalculateMinHeight();
        CalculateMaxHeight();
    }
    public void Notify(OnShootArgs onShootArgs)
    {

        OnShootEventHandler(onShootArgs);
    }

    public void Notify(OnFaileArgs onFailArgs)
    {
        OnFailEventHandler(onFailArgs);
    }

    public void Notify(OnScoreArgs onScoreArgs)
    {
        OnScoreEventHandler(onScoreArgs);
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);

        _instance = this;
        //DontDestroyOnLoad(this);
        RecalculateSettings();
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("MainGameScene");
    }

}
