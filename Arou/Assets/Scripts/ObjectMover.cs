using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private Transform[] _objectsToMove;

    void Update()
    {
        foreach(var obj in _objectsToMove)
            obj.position = new Vector3(transform.position.x, transform.position.y);
    }
}
