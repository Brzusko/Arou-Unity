using System;
using System.Collections.Generic;
using EventArguments;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public event EventHandler<OnDespawnArgs> OnDespawn;
    public event EventHandler<OnSpawnArgs> OnSpawn;
    private LinkedList<Pipe> _despawnedPipes = new LinkedList<Pipe> { };
    private GameManager _manager;

    [Header("Vars")]
    [SerializeField]
    private GameObject _prefabToSpawn = null;

    [Header("Spawner Info")]
    [SerializeField]
    private Transform _spawnTrasform;
    [SerializeField]
    private int _lastPipesSpawnCount = 0;
    public int pipesSpawnCount = 0;
  

    public void OnDespawnHandler(OnDespawnArgs onDespawnArgs)
    {
        OnDespawn?.Invoke(this, onDespawnArgs);
        onDespawnArgs.Pipe.SetNewLocation(CalculatePosition());
        _despawnedPipes.AddLast(onDespawnArgs.Pipe);
    }

    protected virtual void OnScoreHandler(object sender, OnScoreArgs onScoreArgs)
    {
        pipesSpawnCount = _manager.PipeSpawnCount;

        if(pipesSpawnCount > _lastPipesSpawnCount)
        {
            _lastPipesSpawnCount = pipesSpawnCount;
            Spawn();
        }
    }
    public void OnSpawnHandler(OnSpawnArgs onSpawnArgs)
    {
        OnSpawn?.Invoke(this, onSpawnArgs);
        try
        {
            _despawnedPipes.RemoveFirst();

        }
        catch(InvalidOperationException ex)
        {
            Debug.Log("PipeSpawner list is empty");
        }
        
    }

    public bool IsPipeDespawned(string pipeName)
    {
        foreach (var pipe in _despawnedPipes)
            if (pipe.name == pipeName)
                return true;
        return false;
    }

    public void Spawn()
    {
        var spawnedPipe = Instantiate(_prefabToSpawn, CalculatePosition(), Quaternion.identity);
        spawnedPipe.name = $"Pipe{_despawnedPipes.Count + 1}_despawned";
        _despawnedPipes.AddLast(spawnedPipe.GetComponent<Pipe>());
    }

    private Vector3 CalculatePosition()
    {
        if (_despawnedPipes.Count == 0)
            return _spawnTrasform.position;
        else
        {
            return _despawnedPipes.Last.Value.transform.position + new Vector3(0, UnityEngine.Random.Range(_manager.MinPadHeight,_manager.MaxPadHeight), 0);
        }
            
    }

    private void Start()
    {
        _manager = GameManager.Instance;
        _manager.OnScoreEvent += OnScoreHandler;
        pipesSpawnCount = _manager.PipeSpawnCount;
        _lastPipesSpawnCount = pipesSpawnCount;

        for (var it = 0; it < pipesSpawnCount; it++)
            Spawn();
    }

    private void Update()
    {
        Debug.Log(_despawnedPipes.Count);
    }

}
