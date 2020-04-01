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

    private Vector3 RelativePivotPos
    {
        get
        {
            return _rightPipePivot.transform.position - _leftPipePivot.transform.position;
        }
    }

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
        transform.position = location;
    }
    void Start()
    {
        SetCenterOfArea();
        SetSizeOfArea();
    }
}
