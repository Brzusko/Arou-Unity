using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDrawer : MonoBehaviour
{
    private ForceHanlder _forceHanlder;
    [SerializeField]
    private Transform _playerTransform;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Material _material;
    void Start()
    {
        _forceHanlder = GetComponent<ForceHanlder>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.material = _material;
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.SetWidth(0.08f, 0.08f);
    }

    void Update()
    {
        if(_forceHanlder.mouseState == ForceHanlder.MOUSE_STATE.HOLDING)
        {
            var mousePos = _forceHanlder.GetMouseGlobalPos();
            var relativePos = mousePos - _playerTransform.position;

            //Debug.DrawLine(_playerTransform.position, _playerTransform.position + _forceHanlder.Force);
            var linePoints = new Vector3[] { _playerTransform.position, _playerTransform.position + _forceHanlder.Force};

            _lineRenderer.positionCount = linePoints.Length;
            _lineRenderer.SetPositions(linePoints);
        }
        else
        {
            ResetLine();
        }
    }

    private void ResetLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
