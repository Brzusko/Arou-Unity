using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform _playerObj;
    [SerializeField]
    private float _dotProduct = 0.0f;
    private Vector3 _directionToPlayer = Vector3.zero;
    private Vector3 _cameraPosOnSamePlane = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPosOnSamePlane = new Vector3(transform.position.x, transform.position.y);
        _directionToPlayer = (_playerObj.position - _cameraPosOnSamePlane).normalized;

        if((_dotProduct = Vector3.Dot(transform.up, _directionToPlayer)) > 0)
        {
            var newCameraPos = new Vector3(transform.position.x, _playerObj.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newCameraPos, 0.05f);
        }

    }
}
