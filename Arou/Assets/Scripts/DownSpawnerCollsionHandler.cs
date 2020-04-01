﻿using EventArguments;
using UnityEngine;

public class DownSpawnerCollsionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ScoreArea")
        {
            var pipe = collision.transform.parent.GetComponent<Pipe>();
            var eventArgs = new OnDespawnArgs { Pipe = pipe};
            GameManager.Instance.PipeSpawner.OnDespawnHandler(eventArgs);
        }
    }
}