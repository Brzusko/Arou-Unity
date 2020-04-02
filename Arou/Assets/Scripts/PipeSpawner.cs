using System;
using System.Collections.Generic;
using EventArguments;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public event EventHandler<OnDespawnArgs> OnDespawn;
    public event EventHandler<OnSpawnArgs> OnSpawn;
    private LinkedList<Pipe> _despawnedPipes = new LinkedList<Pipe> { };

    [Header("Vars")]
    [SerializeField]
    private GameObject _prefabToSpawn = null;

    [Header("Spawner Info")]
    [SerializeField]
    private Transform _spawnTrasform;
    [SerializeField]
    public int pipesSpawnCount = 0;
  

    public void OnDespawnHandler(OnDespawnArgs onDespawnArgs)
    {
        OnDespawn?.Invoke(this, onDespawnArgs);
        onDespawnArgs.Pipe.SetNewLocation(CalculatePosition());
        _despawnedPipes.AddLast(onDespawnArgs.Pipe);
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
        spawnedPipe.name = $"Pipe_{_despawnedPipes.Count + 1}";
        _despawnedPipes.AddLast(spawnedPipe.GetComponent<Pipe>());
    }

    private Vector3 CalculatePosition()
    {
        if (_despawnedPipes.Count == 0)
            return _spawnTrasform.position;
        else
            return _despawnedPipes.Last.Value.transform.position + new Vector3(0, 10, 0);
    }

    private void Start()
    {
        for (var it = 0; it < pipesSpawnCount; it++)
            Spawn();
    }

}
