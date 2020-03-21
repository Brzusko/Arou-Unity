using EventArguments;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameManager _gameManager;
    private Rigidbody2D _rigidbody2D;

    protected virtual void OnShootHandler(object sender, OnShootArgs onShootArgs)
    {
        _rigidbody2D.velocity = new Vector2(0.0f,0.1f);
        _rigidbody2D.AddForce(onShootArgs.Force * _gameManager.ForceMultipler);
    }
    void Start()
    {
        _gameManager = GameManager.Instance;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager.OnShootEvent += OnShootHandler;
    }

    private void FixedUpdate()
    {
        transform.up = Vector3.Lerp(transform.up, _rigidbody2D.velocity, 0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.velocity = new Vector2(0.0f, 0.1f);
    }
}
