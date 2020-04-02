using System;
using EventArguments;
using UnityEngine;

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

    public PipeSpawner PipeSpawner { get => _pipeSpawner; }
    #endregion

    #region Private settings and vars

    [Header("Main Settings")]
    [SerializeField]
    private int _playerScore = 0;
    [SerializeField]
    private float _maxDistanceBetweenPads = 0.0f;
    [SerializeField]
    private float _forceMultipler = 0.0f;
    [SerializeField]
    private float _maxForce = 0.0f;
    [SerializeField]
    private bool _isDebugModeOn = true;

    [Header("Systems")]
    [SerializeField]
    private PipeSpawner _pipeSpawner;
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
        OnFailEvent?.Invoke(this, onFaileArgs);
    }

    protected virtual void OnScoreEventHandler(OnScoreArgs onScoreArgs)
    {
        _playerScore++;
        var score = new OnScoreArgs { Score = _playerScore };
        OnScoreEvent?.Invoke(this, score);
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
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

}
