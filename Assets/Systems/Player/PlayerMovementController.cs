using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] KeyCode moveLeft = KeyCode.A;
    [SerializeField] KeyCode moveRight = KeyCode.D;
    [SerializeField] KeyCode moveUp = KeyCode.W;
    [SerializeField] KeyCode moveDown = KeyCode.S;

    [SerializeField] Vector3 _playerMoveInput = Vector3.zero;
    [SerializeField] float speed = 5f;
    [SerializeField] float damping = 2f;

    void Update()
    {
        _playerMoveInput = Vector3.zero;
        if (Input.GetKey(moveUp))
        {
            _playerMoveInput += transform.forward;
        }
        if (Input.GetKey(moveDown))
        {
            _playerMoveInput -= transform.forward;
        }
        if (Input.GetKey(moveRight))
        {
            _playerMoveInput += transform.right;
        }
        if (Input.GetKey(moveLeft))
        {
            _playerMoveInput -= transform.right;
        }

        _playerMoveInput.Normalize();
        var movement = _playerMoveInput.normalized * speed * Time.deltaTime;

        _rigidbody.AddForce(movement, ForceMode.VelocityChange);

        // Optional: Apply damping to make the movement snappier
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, damping * Time.deltaTime);

        /*        _rigidbody.AddForce(-_rigidbody.velocity * damping, ForceMode.VelocityChange);
                _rigidbody.AddRelativeForce(_playerMoveInput * speed, ForceMode.Force);*/
        //_rigidbody.AddRelativeForce(_playerMoveInput * speed, ForceMode.Force);
    }
}
