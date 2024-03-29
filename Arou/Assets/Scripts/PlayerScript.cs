﻿using EventArguments;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameManager _gameManager;
    private Rigidbody2D _rigidbody2D;

    protected virtual void OnShootHandler(object sender, OnShootArgs onShootArgs)
    {
        _rigidbody2D.velocity = new Vector2(0.0f,0.1f);
        _rigidbody2D.AddForce(onShootArgs.Force * _gameManager.ForceMultipler);
        _rigidbody2D.gravityScale = _gameManager.Gravity;
    }

    protected virtual void OnFailHandler(object sender, OnFaileArgs onFailArgs)
    {
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.velocity = Vector2.zero;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager.OnShootEvent += OnShootHandler;
        _gameManager.OnFailEvent += OnFailHandler;
        _rigidbody2D.gravityScale = 0.0f;
    }

    private void FixedUpdate()
    {
        transform.up = Vector3.Lerp(transform.up, _rigidbody2D.velocity, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Pipe")
            _gameManager.Notify(new OnFaileArgs { });
        else if (collision.tag == "ScoreArea")
        {
            var pipe = collision.transform.parent.GetComponent<Pipe>();
            if (pipe.IsScorable)
            {
                pipe.Score();
                _gameManager.Notify(new OnScoreArgs { });
            }
        }
    }

}
