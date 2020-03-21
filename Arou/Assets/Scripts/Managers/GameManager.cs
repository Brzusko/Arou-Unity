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
    #endregion

    #region Events
    public event EventHandler<OnShootArgs> OnShootEvent;
    #endregion

    protected virtual void OnShootEventHandler(OnShootArgs onShootArgs)
    {
        OnShootEvent?.Invoke(this, onShootArgs);
        Debug.Log(onShootArgs.Force);
    }

    public void Notify(OnShootArgs onShootArgs)
    {

        OnShootEventHandler(onShootArgs);
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
