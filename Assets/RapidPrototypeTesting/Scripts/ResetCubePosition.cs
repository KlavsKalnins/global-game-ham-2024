using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCubePosition : MonoBehaviour
{
    [SerializeField] Transform _transform;
    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = _transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_transform == null) return;
        if (_transform.transform.position.y <= -10)
        {
            ResetCube();
        }
    }
    void ResetCube()
    {
        if (_transform == null) return;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _transform.transform.position = gameObject.transform.position;
        _rigidbody.isKinematic = false;
    }
}
