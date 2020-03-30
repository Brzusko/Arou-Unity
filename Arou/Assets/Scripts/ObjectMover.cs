using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private Transform _objectToMove;

    void Update()
    {
        _objectToMove.position = new Vector3(transform.position.x, transform.position.y);
    }
}
