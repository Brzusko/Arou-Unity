using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Pipes")]
    [SerializeField]
    private Transform _leftPipeTransform;
    [SerializeField]
    private Transform _rightPipeTransform;
    [SerializeField]
    private Transform _leftPipePivot;
    [SerializeField]
    private Transform _rightPipePivot;
    [Header("Score Area")]
    [SerializeField]
    private Transform _scoreAreaTransform;
    [SerializeField]
    private BoxCollider2D _scoreAreaCollider;
    private bool CanBeScored = true;
    private GameManager _manager;

    private Vector3 RelativePivotPos
    {
        get
        {
            return _rightPipePivot.transform.position - _leftPipePivot.transform.position;
        }
    }

    public bool IsScorable { get => CanBeScored; }

    private void SetCenterOfArea()
    {
        _scoreAreaTransform.position = Vector3.Lerp(_leftPipePivot.position, _leftPipePivot.position + RelativePivotPos, 0.5f);
    }

    private void SetSizeOfArea()
    {
        _scoreAreaCollider.size = new Vector2(RelativePivotPos.magnitude, _scoreAreaCollider.size.y);
    }

    public void SetNewLocation(Vector3 location)
    {
        CanBeScored = true;
        transform.position = location;
        RePipe();
    }

    public void RePipe()
    {
        SetScaleOfPipes();
        SetCenterOfArea();
        SetSizeOfArea();
    }
    private void SetScaleOfPipes()
    {
        var randScale = Random.Range(_manager.MinPadWith, _manager.MaxPadWith);
        var randLerp = Random.Range(0.01f, 1.0f);
        var firstPipeScale = 5 - (randScale * randLerp);
        var secondPipeScale = 5 - (randScale * (1.0f - randLerp));
     
        _leftPipeTransform.localScale = new Vector3(firstPipeScale, 1);
        _rightPipeTransform.localScale = new Vector3(secondPipeScale, 1);
    }
    public void Score()
    {
        CanBeScored = false;
    }
    void Start()
    {
        _manager = GameManager.Instance;
        RePipe();
    }
}
