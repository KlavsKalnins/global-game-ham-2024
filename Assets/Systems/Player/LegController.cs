using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] KeyCode moveLeft = KeyCode.A;
    [SerializeField] KeyCode moveRight = KeyCode.D;
    [SerializeField] KeyCode moveUp = KeyCode.W;
    [SerializeField] KeyCode moveDown = KeyCode.S;

    [SerializeField] Vector3 _playerMoveInput = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(moveLeft))
        {
            _playerMoveInput *= _rigidbody.mass;
            _playerMoveInput *= _rigidbody.mass; // NOTE: For dev purposes.

            _rigidbody.AddRelativeForce(_playerMoveInput, ForceMode.Force);
        }
    }
}
