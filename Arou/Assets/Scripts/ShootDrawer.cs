using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDrawer : MonoBehaviour
{
    private ForceHanlder _forceHanlder;
    [SerializeField]
    private Transform _playerTransform;
    void Start()
    {
        _forceHanlder = GetComponent<ForceHanlder>();
    }

    void Update()
    {
        if(_forceHanlder.mouseState == ForceHanlder.MOUSE_STATE.HOLDING)
        {
            var mousePos = _forceHanlder.GetMouseGlobalPos();
            var relativePos = mousePos - _playerTransform.position;

            Debug.DrawLine(_playerTransform.position, _playerTransform.position + _forceHanlder.Force);
        }
    }
}
